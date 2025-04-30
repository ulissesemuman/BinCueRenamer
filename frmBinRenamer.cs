using Microsoft.VisualBasic.Logging;
using System.IO;
using static System.Windows.Forms.LinkLabel;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Linq;

namespace BinRenamer
{
    public partial class frmBinRenamer : Form
    {
        Dictionary<string, string> acronymToRegion = new Dictionary<string, string>();
        List<Command> commands = new List<Command>();
        string textInfo = string.Empty;
        List<string> trackUpdatedList;
        string trackText;
        List<string> log = new List<string>();
        string errorItem;

        public frmBinRenamer()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            Rename(txtFolderName.Text);

            frmExecutionLog frm = new frmExecutionLog(string.Join("\r\n", log));

            frm.Show();

            log = new List<string>();
        }

        private void btnFolderName_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFolderName.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void frmBinRenamer_Load(object sender, EventArgs e)
        {
            cmbBaseName.SelectedIndex = 0;
            cmbRegionFormat.SelectedIndex = 0;
            trackText = txtCurrentTrackText.Text;

            LoadRegions();
        }

        //TODO: colocar try catch para identificar jogo e local específico. exemplo viuasual novel bakuretsu com cue zoado e também alguma coisa na pasta de rpg japones
        //TODO: mudar a forma das alterações, criar uma temporária com a mesma estrutura com o arquivo cue normal e oas bins vazios, apenas com o nome
        //TODO: criar janela com log de execução no final, apresentando sucesso ou a lista de jogos com erros
        private void Rename(string folderName)
        {
            string trackMask = txtTrackNaming.Text;
            string discMask = txtDiscNaming.Text;
            int padLeftTrack = chkTrackLeadingZeroes.Checked ? 2 : 0;
            int padLefDisc = chkDiscLeadingZeroes.Checked ? 2 : 0;
            string wordDelimiter = string.IsNullOrEmpty(txtWordDelimiter.Text) ? " " : txtWordDelimiter.Text;
            string newWordDelimiter = string.IsNullOrEmpty(txtNewWordDelimiter.Text) ? " " : txtNewWordDelimiter.Text;

            try
            {
                string[] folders = Directory.GetDirectories(folderName);

                if (chkRecursive.Checked && folders.Length > 0)
                {
                    // Procura jogos nas pastas internas
                    foreach (string folder in Directory.GetDirectories(folderName))
                    {
                        Rename(folder);
                    }
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(folderName);

                //DirectoryInfo directoryInfo = new DirectoryInfo(folderName);
                string identification = string.Empty;
                bool hasTrackText = false;
                string trackInfo = string.Empty;
                string discInfo = string.Empty;
                string cueFile = string.Empty;

                // Obtém informações do arquivo cue
                string[] cueFiles = Directory.GetFiles(folderName, "*.cue", SearchOption.TopDirectoryOnly);

                // Faz apenas se tiver um arquivo .cur
                if (cueFiles.Length > 0)
                {
                    if (cueFile.Length > 1) 
                    {
                        throw new Exception("Multiple .cue files in the folder.");
                    }

                    cueFile = cueFiles[0];
                    FileInfo fileInfo = new FileInfo(cueFile);
                    string[] lines = File.ReadAllLines(fileInfo.FullName);

                    // Identifica se o texto da trilha deve estar presente no nome dos arquivos
                    hasTrackText = HasTrackText(lines);

                    // Obtém a descrição limpa do jogo (sem informação de trilha)
                    identification = GetIdentification(identification, cueFile, lines, directoryInfo);

                    //Remove a informação do Disco, agora temos o nome limpo do jogo
                    identification = RemoveDiscWord(identification, discMask, padLefDisc);

                    discInfo = textInfo;

                    // Escolhe o padrão base para nomear todos os itens relacionados ao nome do jogo
                    identification = ChoosePatternType(identification);

                    // Atualiza e renomeia o arquivo .cue
                    fileInfo = UpdateCueFile(identification, fileInfo, lines, trackMask, padLeftTrack, discInfo, hasTrackText);

                    // Renomeia o(s) arquivos .bin
                    RenameBinFiles(identification, folderName, trackMask, padLeftTrack, discInfo, hasTrackText);

                    directoryInfo = RenameFolder(identification, directoryInfo, discInfo);

                    if (wordDelimiter != newWordDelimiter)
                    {
                        fileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, fileInfo.Name));

                        ReplaceDelimiter(directoryInfo, fileInfo.FullName, lines, wordDelimiter, newWordDelimiter);
                    }

                    //int i = 0;
                    //i = i / i;

                    log.Add("SUCCESS - " + folderName);
                }
            }
            catch (Exception ex) 
            {
                log.Add("ERROR - " + folderName + " - " + errorItem + " - " + ex.Message);

                for(int i = commands.Count - 1; i >= 0; i--) 
                {
                    commands[i].Undo();
                }
            }
            finally
            {
                commands = new List<Command>();
            }
        }

        private void LoadRegions()
        {
            acronymToRegion.Add("(US)", "(USA)");
            acronymToRegion.Add("(JP)", "(Japan)");
            acronymToRegion.Add("(EU)", "(Europe)");
        }

        private void ReplaceDelimiter(DirectoryInfo directoryInfo, string cueFile, string[] lines, string wordDelimiter, string newWordDelimiter)
        {
            string[] binFiles = Directory.GetFiles(directoryInfo.FullName, "*.bin", SearchOption.TopDirectoryOnly);
            string newFileName = string.Empty;
            string newCueFileName = string.Empty;
            string newFolderName = string.Empty;
            Command command;
            FileInfo fileInfo;

            foreach (string file in binFiles)
            {
                fileInfo = new FileInfo(file);
                newFileName = fileInfo.Name.Replace(wordDelimiter, newWordDelimiter);

                errorItem = file;

                command = new Command();
                command.NameBefore = file;
                command.NameAfter = Path.Combine(fileInfo.Directory.FullName, newFileName);
                File.Move(command.NameBefore, command.NameAfter);
                command.Type = Command.CommandType.FileMove;
                commands.Add(command);
            }

            fileInfo = new FileInfo(cueFile);
            newFileName = fileInfo.Name.Replace(wordDelimiter, newWordDelimiter);
            newCueFileName = Path.Combine(fileInfo.Directory.FullName, newFileName);

            errorItem = cueFile;

            command = new Command();
            command.NameBefore = cueFile;
            command.NameAfter = newCueFileName;
            File.Move(command.NameBefore, command.NameAfter);
            command.Type = Command.CommandType.FileMove;
            commands.Add(command);

            command = new Command();
            command.Lines = lines;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().StartsWith("FILE", StringComparison.OrdinalIgnoreCase))
                {
                    newFileName = RemoveCueTagWords(lines[i]).Replace("\"", "").Replace(wordDelimiter, newWordDelimiter);

                    lines[i] = "FILE \"" + newFileName + "\" BINARY";
                }
            }

            command.NameBefore = newCueFileName;
            // Salva os dados do arquivo .cue
            File.WriteAllLines(newCueFileName, lines);
            command.Type = Command.CommandType.FileWriteAllLines;
            commands.Add(command);

            newFolderName = directoryInfo.Name.Replace(wordDelimiter, newWordDelimiter);

            command = new Command();
            command.NameBefore = directoryInfo.FullName;
            command.NameAfter = Path.Combine(directoryInfo.Parent.FullName, newFolderName);

            // Altrera o nome da pasta de acordo com as regras definidas                
            if (directoryInfo.Name != newFolderName)
            {
                errorItem = command.NameBefore;

                Directory.Move(command.NameBefore, command.NameAfter);
                command.Type = Command.CommandType.DirectoryMove;
                commands.Add(command);
            }
        }

        private string GetIdentification(string identification, string cueFile, string[] lines, DirectoryInfo directoryInfo)
        {
            if (cueFile.Length > 1)
            {
                // Obtém o nome do jogo sem os indicadores de Disc ou Track
                if (cmbBaseName.SelectedIndex == 0)
                {
                    identification = GetIdentificationByCue(cueFile, lines);
                }
                // Obtem informações da pasta
                else
                {
                    identification = directoryInfo.Name;
                }
            }
            else
            {
                //erro
            }

            return identification;
        }

        // Obtém o nome do jogo sem os indicadores de Disc ou Track
        private string GetIdentificationByCue(string cueFile, string[] lines)
        {
            FileInfo fileInfo = new FileInfo(cueFile);
            string cueIdentification = string.Empty;
            int trackWordStartPosdition = -1;
            int trackWordEndPosdition = -1;

            string cueFirstFileLine = string.Empty;

            foreach (string line in lines)
            {
                if (line.Trim().StartsWith("FILE"))
                {
                    cueFirstFileLine = line;

                    break;
                }
            }

            // Remove todo os dados até sobrar apenas o nome do jogo
            cueIdentification = cueFirstFileLine.Replace(".BIN", "", StringComparison.OrdinalIgnoreCase)
                                            .Replace("\"", "", StringComparison.OrdinalIgnoreCase)
                                            .Trim();

            cueIdentification = RemoveCueTagWords(cueIdentification);

            // Procura pela palavra Track
            trackWordStartPosdition = GetWordStartPosition(cueIdentification, trackText);

            if (trackWordStartPosdition > -1)
            {
                trackWordEndPosdition = cueIdentification.IndexOf(")", trackWordStartPosdition, StringComparison.OrdinalIgnoreCase);

                if (trackWordEndPosdition == -1)
                {
                    trackWordEndPosdition = trackWordStartPosdition + trackText.Length;
                }

                // Remove do nome do jogo a descrição da trilha
                cueIdentification = (cueIdentification.Substring(0, trackWordStartPosdition) + cueIdentification.Substring(trackWordEndPosdition + 1)).Trim();
            }

            return cueIdentification;
        }

        private string RemoveCueTagWords(string cueFileLine)
        {
            // É mais seguro que apenas fazer o replace, pois pode haver a palavra no noime do jogo
            if (cueFileLine.Trim().StartsWith("FILE", StringComparison.OrdinalIgnoreCase))
            {
                cueFileLine = cueFileLine.Substring(cueFileLine.IndexOf("FILE") + "FILE".Length + 1);
            }

            // É mais seguro que apenas fazer o replace, pois pode haver a palavra no noime do jogo
            if (cueFileLine.Trim().EndsWith("BINARY", StringComparison.OrdinalIgnoreCase))
            {
                cueFileLine = cueFileLine.Substring(0, cueFileLine.Length - ("BINARY".Length + 1));
            }

            return cueFileLine;
        }

        private int GetWordStartPosition(string identification, string word)
        {
            int wordStartPosdition = identification.IndexOf("(" + word, StringComparison.OrdinalIgnoreCase);

            if (wordStartPosdition == -1)
            {
                wordStartPosdition = identification.IndexOf("( " + word, StringComparison.OrdinalIgnoreCase);

                if (wordStartPosdition == -1)
                {
                    wordStartPosdition = identification.IndexOf(word, StringComparison.OrdinalIgnoreCase);
                }
            }

            return wordStartPosdition;
        }

        private string RemoveDiscWord(string identification, string discMask, int padLefDisc)
        {
            int identificationLength = identification.Length;
            string[] discTextArray = txtCurrentDiscText.Text.Split(",");

            foreach (string discText in discTextArray) 
            {
                identification = RemoveWord(identification, discText, discMask, padLefDisc);

                if (identificationLength != identification.Length) break;

                identificationLength = identification.Length;
            }

            return identification;
        }

        private string RemoveWord(string identification, string word, string mask, int padLeftCount)
        {
            int wordStartPosdition = GetWordStartPosition(identification, word);
            int wordEndPosdition;
            string text = string.Empty;
            string numberDigits = string.Empty;

            if (wordStartPosdition > -1)
            {
                wordEndPosdition = identification.IndexOf(")", wordStartPosdition, StringComparison.OrdinalIgnoreCase);

                if (wordEndPosdition == -1)
                {
                    wordEndPosdition = wordStartPosdition + word.Length;
                }

                text = identification.Substring(wordStartPosdition, wordEndPosdition - wordStartPosdition + 1);

                numberDigits = text.Replace(word, "", StringComparison.OrdinalIgnoreCase)
                                                .Replace("(", "", StringComparison.OrdinalIgnoreCase)
                                                .Replace(")", "", StringComparison.OrdinalIgnoreCase)
                                                .Replace(" ", "", StringComparison.OrdinalIgnoreCase);


                textInfo = " " + mask.Replace("%1", numberDigits.ToString().PadLeft(padLeftCount, '0'));

                // Remove do nome do jogo o texto
                identification = (identification.Substring(0, wordStartPosdition) + identification.Substring(wordEndPosdition + 1)).Trim();
            }

            return identification;
        }

        private bool HasTrackText(string[] lines)
        {
            int trackTextCount = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().StartsWith("FILE", StringComparison.OrdinalIgnoreCase) && lines[i].Contains(trackText, StringComparison.OrdinalIgnoreCase))
                {
                    trackTextCount++;
                }
            }

            // Só coloca infomação de trilha se tiver mais que uma
            return (trackTextCount > (chkSingleTrackText.Checked ? -1 : 1));
        }

        private string ChoosePatternType(string identification)
        {
            // Decide qual nomenclatura de região será utilizada
            if (cmbRegionFormat.SelectedIndex == 0)
            {
                identification = acronymToRegion.Aggregate(identification, (current, value) =>
                        current.Replace(value.Key, value.Value));
            }
            else
            {
                identification = acronymToRegion.Aggregate(identification, (current, value) =>
                        current.Replace(value.Value, value.Key));
            }

            return identification;
        }

        private FileInfo UpdateCueFile(string identification, FileInfo fileInfo, string[] lines, string trackMask, int padLeftTrack, string discInfo, bool hasTrackText)
        {
            string newCueFileName = string.Empty;
            Command command;

            command = new Command();
            command.Lines = lines;

            // Atualiza os dados do arquivo .cue, caso exista um arquivo com o mesmo nome, altera também
            lines = UpdataCueFileData(identification, fileInfo, lines, trackMask, padLeftTrack, discInfo, hasTrackText);

            errorItem = fileInfo.FullName;

            command.NameBefore = fileInfo.FullName;
            // Salva os dados do arquivo .cue
            File.WriteAllLines(fileInfo.FullName, lines);
            command.Type = Command.CommandType.FileWriteAllLines;
            commands.Add(command);

            newCueFileName = Path.Combine(fileInfo.DirectoryName, identification + discInfo + ".cue");

            errorItem = fileInfo.FullName;

            command = new Command();
            command.NameBefore = fileInfo.FullName;
            command.NameAfter = newCueFileName;
            // Altera o nome do arquivo .cue
            File.Move(command.NameBefore, command.NameAfter);
            command.Type = Command.CommandType.FileMove;
            commands.Add(command);

            return new FileInfo(newCueFileName);
        }

        private string[] UpdataCueFileData(string identification, FileInfo fileInfo, string[] lines, string trackMask, int padLeftTrack, string discInfo, bool hasTrackText)
        {
            int trackCount = 1;
            string trackInfo = string.Empty;
            string trackFileName = string.Empty;
            trackUpdatedList = new List<string>();
            string newBinFileName = string.Empty;
            Command command;
            bool validCueFile = false;

            // Atualiza as linhas de arquivos de trilha dentro do arquivo cue
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().StartsWith("FILE", StringComparison.OrdinalIgnoreCase))
                {
                    trackInfo = string.Empty;

                    // Remove todo os dados até sobrar apenas o nome do jogo
                    trackFileName = lines[i].Replace("\"", "", StringComparison.OrdinalIgnoreCase).Trim();

                    trackFileName = RemoveCueTagWords(trackFileName);

                    // Gera a descrição de disco apenas se já havia sido encontrada essa informação antes
                    if (hasTrackText)
                    {
                        trackInfo = " " + trackMask.Replace("%1", trackCount.ToString().PadLeft(padLeftTrack, '0'));
                    }

                    // Ajusta a linha de acordo com as regras definidas
                    lines[i] = "FILE \"" + identification + discInfo + trackInfo + ".bin\" BINARY";

                    // Se já existe um arquivo de trilha correspondente à entrada no arquivo cue, já renomeia
                    if (File.Exists(Path.Combine(fileInfo.DirectoryName, discInfo, trackFileName)))
                    {
                        newBinFileName = identification + discInfo + trackInfo + ".bin";

                        errorItem = Path.Combine(fileInfo.DirectoryName, trackFileName);

                        command = new Command();
                        command.NameBefore = Path.Combine(fileInfo.DirectoryName, trackFileName);
                        command.NameAfter = Path.Combine(fileInfo.DirectoryName, newBinFileName);
                        File.Move(command.NameBefore, command.NameAfter);
                        command.Type = Command.CommandType.FileMove;
                        commands.Add(command);

                        trackUpdatedList.Add(Path.Combine(fileInfo.DirectoryName, newBinFileName));
                    }

                    validCueFile = true;

                    if (!hasTrackText) break;

                    trackCount++;
                }
            }

            if (!validCueFile)
            {
                errorItem = fileInfo.FullName;

                throw new Exception("Invalid .cue file.");
            }

            return lines;
        }
        private void RenameBinFiles(string identification, string folder, string trackMask, int padLeftTrack, string discInfo, bool hasTrackText)
        {
            // Obtém todos os arquivos de trilha na pasta
            string[] binFiles = Directory.GetFiles(folder, "*.bin", SearchOption.TopDirectoryOnly);

            string binFileName = string.Empty;
            string newBinFileName = string.Empty;
            string trackNumberDigits = string.Empty;
            string currentTrackInfo = string.Empty;
            string trackInfo = string.Empty;
            int trackWordStartPosdition = -1;
            int trackWordEndPosdition = -1;
            FileInfo fileInfo;
            Command command;

            // Entra apenas se foi entrada a palavra Track antes, para poder renomear os arquivos de trilha
            if (hasTrackText)
            {
                //Para cada arquivo gera o novo nome de acordo com as regras e renomeia
                foreach (string file in binFiles)
                {
                    fileInfo = new FileInfo(file);
                    binFileName = fileInfo.Name;

                    //// Se já foi alterado um arquivo de trilha anteriormente, não altera novamente
                    if (!trackUpdatedList.Contains(file))
                    {
                        trackWordStartPosdition = GetWordStartPosition(binFileName, trackText);

                        trackWordEndPosdition = binFileName.IndexOf(")", trackWordStartPosdition, StringComparison.OrdinalIgnoreCase);

                        if (trackWordEndPosdition == -1)
                        {
                            trackWordEndPosdition = trackWordStartPosdition + trackText.Length;
                        }

                        currentTrackInfo = binFileName.Substring(trackWordStartPosdition, trackWordEndPosdition - trackWordStartPosdition + 1);

                        trackNumberDigits = currentTrackInfo.Replace(trackText, "", StringComparison.OrdinalIgnoreCase)
                                                        .Replace("(", "", StringComparison.OrdinalIgnoreCase)
                                                        .Replace(")", "", StringComparison.OrdinalIgnoreCase)
                                                        .Replace(" ", "", StringComparison.OrdinalIgnoreCase);

                        trackInfo = " " + trackMask.Replace("%1", trackNumberDigits.ToString().PadLeft(padLeftTrack, '0'));

                        newBinFileName = identification + discInfo + trackInfo;

                        errorItem = fileInfo.FullName;

                        command = new Command();
                        command.NameBefore = fileInfo.FullName;
                        command.NameAfter = Path.Combine(fileInfo.DirectoryName, newBinFileName + ".bin");
                        File.Move(command.NameBefore, command.NameAfter);
                        command.Type = Command.CommandType.FileMove;
                        commands.Add(command);
                    }
                }
            }
            // Caso não haja informação de trilha, grava o arquivo sem essa informação
            else
            {
                if (binFiles.Length == 1)
                {
                    // Se já foi alterado um arquivo de trilha anteriormente, não altera novamente
                    if (!trackUpdatedList.Contains(binFiles[0]))
                    {
                        fileInfo = new FileInfo(binFiles[0]);

                        newBinFileName = identification + discInfo;

                        errorItem = fileInfo.FullName;

                        command = new Command();
                        command.NameBefore = fileInfo.FullName;
                        command.NameAfter = Path.Combine(fileInfo.DirectoryName, newBinFileName + ".bin");
                        File.Move(command.NameBefore, command.NameAfter);
                        command.Type = Command.CommandType.FileMove;
                        commands.Add(command);
                    }
                }
                else
                {
                    errorItem = binFiles[0] + " / " + binFiles[1];

                    throw new Exception("Multiple bin files for a single track.");
                }
            }
        }

        private DirectoryInfo RenameFolder(string identification, DirectoryInfo directoryInfo, string discInfo)
        {
            Command command = new Command();
            command.NameBefore = directoryInfo.FullName;
            command.NameAfter = Path.Combine(directoryInfo.Parent.FullName, identification + discInfo);

            // Altrera o nome da pasta de acordo com as regras definidas                
            if (command.NameBefore != command.NameAfter)
            {
                errorItem = command.NameBefore;

                Directory.Move(command.NameBefore, command.NameAfter);
                command.Type = Command.CommandType.DirectoryMove;
                commands.Add(command);
            }

            return new DirectoryInfo(command.NameAfter);
        }

        private void txtCurrentTrackText_Leave(object sender, EventArgs e)
        {
            trackText = txtCurrentTrackText.Text;
        }
    }
}
