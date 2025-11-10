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
            if (!IsRunningAsAdministrator())
            {
                MessageBox.Show("Este software deve ser executado como Administrador para funcionar corretamente.\n\n" +
                               "Feche o programa e execute novamente clicando com o botão direito e selecionando 'Executar como Administrador'.",
                               "Privilégios Administrativos Necessários",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);

                Application.Exit();
                return;
            }

            MessageBox.Show("Crie um Ponto de Restauração antes de usar o software", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool IsRunningAsAdministrator()
        {
            try
            {
                using (var identity = System.Security.Principal.WindowsIdentity.GetCurrent())
                {
                    var principal = new System.Security.Principal.WindowsPrincipal(identity);
                    return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
                }
            }
            catch
            {
                return false;
            }
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
                "notepadplusplus",
                "7zip"
            };

            UpdateProgressBar(0, "Iniciando processo de instalação...");

            UpdateProgressBar(0, "Passo 1/3: Verificando Chocolatey...");
            bool chocolateyInstalled = await CheckChocolateySimpleAsync();

            if (!chocolateyInstalled)
            {
                UpdateProgressBar(10, "Instalando Chocolatey...");
                bool installSuccess = await InstallChocolateyAsync();

                if (!installSuccess)
                {
                    UpdateProgressBar(0, "Falha na instalação do Chocolatey");
                    MessageBox.Show("Falha na instalação do Chocolatey. O processo será interrompido.", "Erro",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ResetButton();
                    return;
                }

                await Task.Delay(5000);
                chocolateyInstalled = await CheckChocolateySimpleAsync();

                if (!chocolateyInstalled)
                {
                    UpdateProgressBar(0, "Chocolatey não foi instalado corretamente");
                    MessageBox.Show("Chocolatey não foi instalado corretamente. Tente instalar manualmente.", "Erro",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ResetButton();
                    return;
                }

                UpdateProgressBar(20, "Chocolatey instalado com sucesso!");
                await Task.Delay(2000);
            }
            else
            {
                UpdateProgressBar(20, "Chocolatey já está instalado");
            }

            UpdateProgressBar(20, "Passo 2/3: Configurando Chocolatey...");
            bool configSuccess = await ConfigureChocolateyAsync();

            if (configSuccess)
            {
                UpdateProgressBar(30, "Chocolatey configurado com sucesso");
            }
            else
            {
                UpdateProgressBar(30, "Aviso: Configuração falhou, usando modo alternativo");
            }
            await Task.Delay(1000);

            UpdateProgressBar(30, "Passo 3/3: Instalando programas...");

            int installedCount = 0;
            for (int i = 0; i < programs.Length; i++)
            {
                string program = programs[i];
                int progress = 30 + (i * 70) / programs.Length;

                UpdateProgressBar(progress, $"Instalando {program}... ({i + 1}/{programs.Length})");

                bool installSuccess = await InstallProgramAsync(program);

                if (installSuccess)
                {
                    installedCount++;
                    progress = 30 + ((i + 1) * 70) / programs.Length;
                    UpdateProgressBar(progress, $"{program} instalado! ({installedCount}/{programs.Length})");
                }
                else
                {
                    UpdateProgressBar(progress, $"Falha na instalação do {program}. Continuando...");
                }

                await Task.Delay(1500);
            }

            UpdateProgressBar(100, $"Concluído! {installedCount}/{programs.Length} programas instalados.");

            ResetButton();

            MessageBox.Show($"Processo concluído! {installedCount} de {programs.Length} programas instalados com sucesso.",
                           "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ResetButton()
        {
            if (btnInstallProg.InvokeRequired)
            {
                btnInstallProg.Invoke(new Action(ResetButton));
                return;
            }

            btnInstallProg.Enabled = true;
            progressBar.Value = 0;
            lblStatus.Text = "Pronto para instalar";
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

        private async Task<bool> CheckChocolateySimpleAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var process = new Process())
                    {
                        process.StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = "/c choco --version",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden
                        };

                        process.Start();
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();
                        process.WaitForExit(10000);

                        bool installed = process.ExitCode == 0 && !string.IsNullOrWhiteSpace(output) && !output.Contains("não é reconhecido");

                        System.Diagnostics.Debug.WriteLine($"Chocolatey check - ExitCode: {process.ExitCode}, Output: {output}, Error: {error}");

                        return installed;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erro ao verificar Chocolatey: {ex.Message}");
                    return false;
                }
            });
        }

        private async Task<bool> InstallChocolateyAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    string powerShellCommand = @"[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))";

                    using (var process = new Process())
                    {
                        process.StartInfo = new ProcessStartInfo
                        {
                            FileName = "powershell.exe",
                            Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{powerShellCommand}\"",
                            UseShellExecute = true,
                            CreateNoWindow = false,
                            WindowStyle = ProcessWindowStyle.Normal,
                            Verb = "runas"
                        };

                        process.Start();

                        if (process.WaitForExit(180000))
                        {
                            System.Diagnostics.Debug.WriteLine($"Chocolatey install completed. Exit code: {process.ExitCode}");
                            return process.ExitCode == 0;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Chocolatey install timeout");
                            process.Kill();
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Exceção na instalação do Chocolatey: {ex.Message}");
                    return false;
                }
            });
        }

        private async Task<bool> ConfigureChocolateyAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var process = new Process())
                    {
                        process.StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = "/c choco feature enable -n allowGlobalConfirmation",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden
                        };

                        process.Start();
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();
                        process.WaitForExit(30000); // 30 segundos timeout

                        bool success = process.ExitCode == 0;
                        System.Diagnostics.Debug.WriteLine($"Chocolatey configure - ExitCode: {process.ExitCode}, Output: {output}, Error: {error}");

                        return success;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erro ao configurar Chocolatey: {ex.Message}");
                    return false;
                }
            });
        }

        private async Task<bool> InstallProgramAsync(string programName)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var process = new Process())
                    {
                        process.StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = $"/c choco install {programName} -y --force --accept-license --no-progress",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden
                        };

                        process.Start();

                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();

                        if (process.WaitForExit(240000))
                        {
                            System.Diagnostics.Debug.WriteLine($"{programName} install - ExitCode: {process.ExitCode}");

                            bool success = process.ExitCode == 0 || process.ExitCode == 1641 || process.ExitCode == 3010;

                            if (!success)
                            {
                                System.Diagnostics.Debug.WriteLine($"Output: {output}");
                                System.Diagnostics.Debug.WriteLine($"Error: {error}");
                            }

                            return success;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"{programName} install timeout");
                            process.Kill();
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Exceção na instalação do {programName}: {ex.Message}");
                    return false;
                }
            });
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

                MessageBox.Show("Chaves 'Bags' e 'BagMRU' removidas e 'FolderType=NotSpecified' criada com sucesso.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
