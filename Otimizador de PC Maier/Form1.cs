using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Otimizador_de_PC_Maier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Crie um Ponto de Restauração antes de usar o software", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnInstallProg_Click(object sender, EventArgs e)
        {
            btnInstallProg.Enabled = false;

            Task.Run(() => InstallPrograms());
        }

        private async void InstallPrograms()
        {
            string[] programs = {
                "vcredist2005",
                "vcredist2008",
                "vcredist2010",
                "vcredist2012",
                "vcredist2013",
                "vcredist140",
                "firefox",
                "discord",
                "steam",
                "spotify",
                "directx",
                "msiafterburner",
                "vlc",
                "imageglass",
                "notepadplusplus"
            };

            UpdateProgressBar(0, "Verificando Chocolatey...");

            if (!IsChocolateyInstalled())
            {
                UpdateProgressBar(0, "Instalando Chocolatey...");
                InstallChocolatey();

                await Task.Delay(3000);

                UpdateProgressBar(10, "Configurando Chocolatey...");
                EnableChocolateyAutoConfirm();
            }
            else
            {
                UpdateProgressBar(10, "Chocolatey já está instalado");
            }

            for (int i = 0; i < programs.Length; i++)
            {
                string program = programs[i];
                int progress = 10 + (i * 90) / programs.Length;

                UpdateProgressBar(progress, $"Instalando {program}... ({i + 1}/{programs.Length})");

                await InstallProgram(program);

                progress = 10 + ((i + 1) * 90) / programs.Length;
                UpdateProgressBar(progress, $"{program} instalado! ({i + 1}/{programs.Length})");
            }

            UpdateProgressBar(100, "Todos os programas foram instalados");

            btnInstallProg.Invoke(new Action(() =>
            {
                btnInstallProg.Enabled = true;
                progressBar.Value = 0;
                lblStatus.Text = "Pronto para instalar";
            }));

            MessageBox.Show("Todos os programas foram instalados", "Concluído",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void UpdateProgressBar(int value, string status)
        {
            if (progressBar.InvokeRequired || lblStatus.InvokeRequired)
            {
                progressBar.Invoke(new Action<int, string>(UpdateProgressBar), value, status);
                return;
            }

            progressBar.Value = value;
            lblStatus.Text = status;

            Application.DoEvents();
        }
        private void EnableChocolateyAutoConfirm()
        {
            string command = "choco feature enable -n allowGlobalConfirmation";
            ExecutePowerShellCommand(command);
        }

        private bool IsChocolateyInstalled()
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "choco",
                        Arguments = "--version",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                process.WaitForExit();
                return process.ExitCode == 0;
            }
            catch
            {
                return false;
            }
        }

        private void InstallChocolatey()
        {
            string powerShellCommand = "Set-ExecutionPolicy Bypass -Scope Process -Force; " +
                                     "[System.Net.ServicePointManager]::SecurityProtocol = " +
                                     "[System.Net.ServicePointManager]::SecurityProtocol -bor 3072; " +
                                     "iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))";

            ExecutePowerShellCommand(powerShellCommand);
        }

        private Task InstallProgram(string programName)
        {
            return Task.Run(() =>
            {
                string command = $"choco install {programName} --force";
                ExecutePowerShellCommand(command);
            });
        }

        private void ExecutePowerShellCommand(string command)
        {
            try
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{command}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = new Process())
                {
                    process.StartInfo = processStartInfo;

                    process.OutputDataReceived += (s, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            System.Diagnostics.Debug.WriteLine(e.Data);
                        }
                    };

                    process.ErrorDataReceived += (s, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            System.Diagnostics.Debug.WriteLine($"ERRO: {e.Data}");
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exceção: {ex.Message}");
            }
        }

        private void btnLinkDriverAMD_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.amd.com/pt/support/download/drivers.html");
        }

        private async void btnAMDDisableMPO_Click(object sender, EventArgs e)
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = "/c reg.exe add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\Dwm\" /v \"OverlayTestMode\" /t REG_DWORD /d \"5\" /f";
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    await Task.Run(() => process.WaitForExit());

                    if (process.ExitCode == 0)
                        MessageBox.Show("MPO desativado com sucesso");
                    else
                        MessageBox.Show("Falha ao aplicar configuração");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private void btnAMDDisableDXNAVI_Click(object sender, EventArgs e)
        {
            if (File.Exists("files/amd/disable_dx11navi.exe"))
            {
                System.Diagnostics.Process process = new Process();
                process.StartInfo.FileName = "disable_dx11navi.exe";
                process.StartInfo.WorkingDirectory = "files/amd";
                process.StartInfo.Verb = "runas";
                process.Start();
                process.WaitForExit();
            }
            else
            {
                MessageBox.Show("Arquivo não encontrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAMDSlimmer_Click(object sender, EventArgs e)
        {
            if (File.Exists("files/amd/radeon slimmer/RadeonSoftwareSlimmer.exe"))
            {
                System.Diagnostics.Process process = new Process();
                process.StartInfo.FileName = "RadeonSoftwareSlimmer.exe";
                process.StartInfo.WorkingDirectory = "files/amd/radeon slimmer";
                process.Start();
                process.WaitForExit();
            }
            else
            {
                MessageBox.Show("Arquivo não encontrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLinkDriverNVIDIA_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.nvidia.com/pt-br/drivers/");
        }

        private void btnMassGrave_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new Process();
            process.StartInfo.FileName = "powershell.exe";
            process.StartInfo.Arguments = "irm https://get.activated.win | iex";
            process.Start();
            process.WaitForExit();
        }

        private void btnUltimatePerformance_Click(object sender, EventArgs e)
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = "/c powercfg -duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61";
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    process.Start();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                        MessageBox.Show("Ultimate Performance habilitado, ative no plano de energia");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private async void btnReduceAudioLatency_Click(object sender, EventArgs e)
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = "/c reg.exe add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Tasks\\Games\" /v \"Scheduling Category\" /t REG_SZ /d \"High\" /f";
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    await Task.Run(() => process.WaitForExit());

                    if (process.ExitCode == 0)
                        MessageBox.Show("Prioridade de Áudio aplicada com sucesso");
                    else
                        MessageBox.Show("Falha ao aplicar configuração");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private async void btnHighPriority_Click(object sender, EventArgs e)
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = "/c reg.exe add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\PriorityControl\" /v \"Win32PrioritySeparation\" /t REG_DWORD /d \"26\" /f";
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    await Task.Run(() => process.WaitForExit());

                    if (process.ExitCode == 0)
                        MessageBox.Show("Alta Prioridade em primeiro plano aplicada com sucesso");
                    else
                        MessageBox.Show("Falha ao aplicar configuração");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private async void btnNetworkThrottling_Click(object sender, EventArgs e)
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = "/c reg.exe add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\" /v \"NetworkThrottlingIndex\" /t REG_DWORD /d \"4294967295\" /f";
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    await Task.Run(() => process.WaitForExit());

                    if (process.ExitCode == 0)
                        MessageBox.Show("Limitação de rede removida com sucesso");
                    else
                        MessageBox.Show("Falha ao aplicar configuração");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private async void btnResponsiveness_Click(object sender, EventArgs e)
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = "/c reg.exe add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\" /v \"SystemResponsiveness\" /t REG_DWORD /d \"15\" /f";
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    await Task.Run(() => process.WaitForExit());

                    if (process.ExitCode == 0)
                        MessageBox.Show("Maior processamento de Mídia aplicado com sucesso");
                    else
                        MessageBox.Show("Falha ao aplicar configuração");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private void btnPowerPlanSettings_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("control.exe", "/name Microsoft.PowerOptions");
        }

        private void btnReboot_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reiniciar agora?", "Confirmação",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "shutdown",
                    Arguments = "/r /t 0",
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                System.Diagnostics.Process.Start(psi);
            }
        }

        private void btnNVIDIACleanstall_Click(object sender, EventArgs e)
        {
            if (File.Exists("files/nvidia/NVCleanstall_1.19.0.exe"))
            {
                System.Diagnostics.Process process = new Process();
                process.StartInfo.FileName = "NVCleanstall_1.19.0.exe";
                process.StartInfo.WorkingDirectory = "files/nvidia";
                process.Start();
                process.WaitForExit();
            }
            else
            {
                MessageBox.Show("Arquivo não encontrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDisableSuperFetch_Click(object sender, EventArgs e)
        {
            try
            {
                using (System.Diagnostics.Process process = new System.Diagnostics.Process())
                {
                    process.StartInfo.FileName = "powershell.exe";
                    process.StartInfo.Arguments = "Stop-Service -Name SysMain -Force";
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.WaitForExit();
                }

                MessageBox.Show("Serviço SuperFetch desativado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                using (System.Diagnostics.Process process = new System.Diagnostics.Process())
                {
                    process.StartInfo.FileName = "powershell.exe";
                    process.StartInfo.Arguments = "Set-Service -Name SysMain -StartupType Disabled";
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.WaitForExit();
                }

                MessageBox.Show("Inicialização do serviço SuperFetch desativada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao desativar o serviço SuperFetch:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnExplorerFolderType_Click(object sender, EventArgs e)
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = "/c reg delete \"HKEY_CURRENT_USER\\Software\\Classes\\Local Settings\\Software\\Microsoft\\Windows\\Shell\\Bags\" /f";
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    await Task.Run(() => process.WaitForExit());
                }

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = "/c reg delete \"HKEY_CURRENT_USER\\Software\\Classes\\Local Settings\\Software\\Microsoft\\Windows\\Shell\\BagMRU\" /f";
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    await Task.Run(() => process.WaitForExit());
                }

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = "/c reg add \"HKEY_CURRENT_USER\\Software\\Classes\\Local Settings\\Software\\Microsoft\\Windows\\Shell\\Bags\\AllFolders\\Shell\" /v FolderType /t REG_SZ /d NotSpecified /f";
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    await Task.Run(() => process.WaitForExit());
                }

                MessageBox.Show("Chaves 'Bags' e 'BagMRU' removidas e 'FolderType=NotSpecified' criada com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
