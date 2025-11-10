namespace Otimizador_de_PC_Maier
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.postFormatGB = new System.Windows.Forms.GroupBox();
            this.btnInstallProg = new System.Windows.Forms.Button();
            this.AMDGB = new System.Windows.Forms.GroupBox();
            this.btnAMDSlimmer = new System.Windows.Forms.Button();
            this.btnAMDDisableDXNAVI = new System.Windows.Forms.Button();
            this.btnLinkDriverAMD = new System.Windows.Forms.Button();
            this.btnAMDDisableMPO = new System.Windows.Forms.Button();
            this.NVIDIAGB = new System.Windows.Forms.GroupBox();
            this.btnNVIDIACleanstall = new System.Windows.Forms.Button();
            this.btnLinkDriverNVIDIA = new System.Windows.Forms.Button();
            this.windowsGB = new System.Windows.Forms.GroupBox();
            this.btnExplorerFolderType = new System.Windows.Forms.Button();
            this.btnDisableSuperFetch = new System.Windows.Forms.Button();
            this.btnPowerPlanSettings = new System.Windows.Forms.Button();
            this.btnResponsiveness = new System.Windows.Forms.Button();
            this.btnNetworkThrottling = new System.Windows.Forms.Button();
            this.btnHighPriority = new System.Windows.Forms.Button();
            this.btnReduceAudioLatency = new System.Windows.Forms.Button();
            this.btnMassGrave = new System.Windows.Forms.Button();
            this.btnUltimatePerformance = new System.Windows.Forms.Button();
            this.btnReboot = new System.Windows.Forms.Button();
            this.labelSign = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.postFormatGB.SuspendLayout();
            this.AMDGB.SuspendLayout();
            this.NVIDIAGB.SuspendLayout();
            this.windowsGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // postFormatGB
            // 
            this.postFormatGB.Controls.Add(this.btnInstallProg);
            this.postFormatGB.Location = new System.Drawing.Point(8, 6);
            this.postFormatGB.Name = "postFormatGB";
            this.postFormatGB.Size = new System.Drawing.Size(264, 108);
            this.postFormatGB.TabIndex = 0;
            this.postFormatGB.TabStop = false;
            this.postFormatGB.Text = "Pós Formatação";
            // 
            // btnInstallProg
            // 
            this.btnInstallProg.Location = new System.Drawing.Point(34, 29);
            this.btnInstallProg.Name = "btnInstallProg";
            this.btnInstallProg.Size = new System.Drawing.Size(196, 50);
            this.btnInstallProg.TabIndex = 0;
            this.btnInstallProg.Text = "Instalar Programas";
            this.btnInstallProg.UseVisualStyleBackColor = true;
            this.btnInstallProg.Click += new System.EventHandler(this.btnInstallProg_Click);
            // 
            // AMDGB
            // 
            this.AMDGB.Controls.Add(this.btnAMDSlimmer);
            this.AMDGB.Controls.Add(this.btnAMDDisableDXNAVI);
            this.AMDGB.Controls.Add(this.btnLinkDriverAMD);
            this.AMDGB.Location = new System.Drawing.Point(8, 120);
            this.AMDGB.Name = "AMDGB";
            this.AMDGB.Size = new System.Drawing.Size(264, 216);
            this.AMDGB.TabIndex = 1;
            this.AMDGB.TabStop = false;
            this.AMDGB.Text = "AMD Radeon";
            // 
            // btnAMDSlimmer
            // 
            this.btnAMDSlimmer.Location = new System.Drawing.Point(34, 139);
            this.btnAMDSlimmer.Name = "btnAMDSlimmer";
            this.btnAMDSlimmer.Size = new System.Drawing.Size(196, 50);
            this.btnAMDSlimmer.TabIndex = 4;
            this.btnAMDSlimmer.Text = "Radeon Slimmer";
            this.btnAMDSlimmer.UseVisualStyleBackColor = true;
            this.btnAMDSlimmer.Click += new System.EventHandler(this.btnAMDSlimmer_Click);
            // 
            // btnAMDDisableDXNAVI
            // 
            this.btnAMDDisableDXNAVI.Location = new System.Drawing.Point(34, 83);
            this.btnAMDDisableDXNAVI.Name = "btnAMDDisableDXNAVI";
            this.btnAMDDisableDXNAVI.Size = new System.Drawing.Size(196, 50);
            this.btnAMDDisableDXNAVI.TabIndex = 3;
            this.btnAMDDisableDXNAVI.Text = "Desativar DXNAVI";
            this.btnAMDDisableDXNAVI.UseVisualStyleBackColor = true;
            this.btnAMDDisableDXNAVI.Click += new System.EventHandler(this.btnAMDDisableDXNAVI_Click);
            // 
            // btnLinkDriverAMD
            // 
            this.btnLinkDriverAMD.Location = new System.Drawing.Point(34, 27);
            this.btnLinkDriverAMD.Name = "btnLinkDriverAMD";
            this.btnLinkDriverAMD.Size = new System.Drawing.Size(196, 50);
            this.btnLinkDriverAMD.TabIndex = 1;
            this.btnLinkDriverAMD.Text = "Link AMD Drivers";
            this.btnLinkDriverAMD.UseVisualStyleBackColor = true;
            this.btnLinkDriverAMD.Click += new System.EventHandler(this.btnLinkDriverAMD_Click);
            // 
            // btnAMDDisableMPO
            // 
            this.btnAMDDisableMPO.Location = new System.Drawing.Point(275, 193);
            this.btnAMDDisableMPO.Name = "btnAMDDisableMPO";
            this.btnAMDDisableMPO.Size = new System.Drawing.Size(230, 50);
            this.btnAMDDisableMPO.TabIndex = 2;
            this.btnAMDDisableMPO.Text = "Desativar MPO";
            this.btnAMDDisableMPO.UseVisualStyleBackColor = true;
            this.btnAMDDisableMPO.Click += new System.EventHandler(this.btnAMDDisableMPO_Click);
            // 
            // NVIDIAGB
            // 
            this.NVIDIAGB.Controls.Add(this.btnNVIDIACleanstall);
            this.NVIDIAGB.Controls.Add(this.btnLinkDriverNVIDIA);
            this.NVIDIAGB.Location = new System.Drawing.Point(8, 342);
            this.NVIDIAGB.Name = "NVIDIAGB";
            this.NVIDIAGB.Size = new System.Drawing.Size(265, 155);
            this.NVIDIAGB.TabIndex = 2;
            this.NVIDIAGB.TabStop = false;
            this.NVIDIAGB.Text = "NVIDIA";
            // 
            // btnNVIDIACleanstall
            // 
            this.btnNVIDIACleanstall.Location = new System.Drawing.Point(34, 80);
            this.btnNVIDIACleanstall.Name = "btnNVIDIACleanstall";
            this.btnNVIDIACleanstall.Size = new System.Drawing.Size(196, 50);
            this.btnNVIDIACleanstall.TabIndex = 7;
            this.btnNVIDIACleanstall.Text = "NVCleanstall";
            this.btnNVIDIACleanstall.UseVisualStyleBackColor = true;
            this.btnNVIDIACleanstall.Click += new System.EventHandler(this.btnNVIDIACleanstall_Click);
            // 
            // btnLinkDriverNVIDIA
            // 
            this.btnLinkDriverNVIDIA.Location = new System.Drawing.Point(34, 24);
            this.btnLinkDriverNVIDIA.Name = "btnLinkDriverNVIDIA";
            this.btnLinkDriverNVIDIA.Size = new System.Drawing.Size(196, 50);
            this.btnLinkDriverNVIDIA.TabIndex = 5;
            this.btnLinkDriverNVIDIA.Text = "Link NVIDIA Drivers";
            this.btnLinkDriverNVIDIA.UseVisualStyleBackColor = true;
            this.btnLinkDriverNVIDIA.Click += new System.EventHandler(this.btnLinkDriverNVIDIA_Click);
            // 
            // windowsGB
            // 
            this.windowsGB.Controls.Add(this.btnExplorerFolderType);
            this.windowsGB.Controls.Add(this.btnDisableSuperFetch);
            this.windowsGB.Controls.Add(this.btnPowerPlanSettings);
            this.windowsGB.Controls.Add(this.btnResponsiveness);
            this.windowsGB.Controls.Add(this.btnAMDDisableMPO);
            this.windowsGB.Controls.Add(this.btnNetworkThrottling);
            this.windowsGB.Controls.Add(this.btnHighPriority);
            this.windowsGB.Controls.Add(this.btnReduceAudioLatency);
            this.windowsGB.Controls.Add(this.btnMassGrave);
            this.windowsGB.Controls.Add(this.btnUltimatePerformance);
            this.windowsGB.Location = new System.Drawing.Point(278, 6);
            this.windowsGB.Name = "windowsGB";
            this.windowsGB.Size = new System.Drawing.Size(569, 330);
            this.windowsGB.TabIndex = 3;
            this.windowsGB.TabStop = false;
            this.windowsGB.Text = "Windows";
            // 
            // btnExplorerFolderType
            // 
            this.btnExplorerFolderType.Location = new System.Drawing.Point(275, 249);
            this.btnExplorerFolderType.Name = "btnExplorerFolderType";
            this.btnExplorerFolderType.Size = new System.Drawing.Size(230, 50);
            this.btnExplorerFolderType.TabIndex = 16;
            this.btnExplorerFolderType.Text = "Desativar Descoberta de Pastas";
            this.btnExplorerFolderType.UseVisualStyleBackColor = true;
            this.btnExplorerFolderType.Click += new System.EventHandler(this.btnExplorerFolderType_Click);
            // 
            // btnDisableSuperFetch
            // 
            this.btnDisableSuperFetch.Location = new System.Drawing.Point(275, 137);
            this.btnDisableSuperFetch.Name = "btnDisableSuperFetch";
            this.btnDisableSuperFetch.Size = new System.Drawing.Size(230, 50);
            this.btnDisableSuperFetch.TabIndex = 15;
            this.btnDisableSuperFetch.Text = "Desativar SuperFetch";
            this.btnDisableSuperFetch.UseVisualStyleBackColor = true;
            this.btnDisableSuperFetch.Click += new System.EventHandler(this.btnDisableSuperFetch_Click);
            // 
            // btnPowerPlanSettings
            // 
            this.btnPowerPlanSettings.Location = new System.Drawing.Point(39, 137);
            this.btnPowerPlanSettings.Name = "btnPowerPlanSettings";
            this.btnPowerPlanSettings.Size = new System.Drawing.Size(230, 50);
            this.btnPowerPlanSettings.TabIndex = 14;
            this.btnPowerPlanSettings.Text = "Trocar Plano de Energia";
            this.btnPowerPlanSettings.UseVisualStyleBackColor = true;
            this.btnPowerPlanSettings.Click += new System.EventHandler(this.btnPowerPlanSettings_Click);
            // 
            // btnResponsiveness
            // 
            this.btnResponsiveness.Location = new System.Drawing.Point(275, 81);
            this.btnResponsiveness.Name = "btnResponsiveness";
            this.btnResponsiveness.Size = new System.Drawing.Size(230, 50);
            this.btnResponsiveness.TabIndex = 13;
            this.btnResponsiveness.Text = "Maior Processamento de Mídia";
            this.btnResponsiveness.UseVisualStyleBackColor = true;
            this.btnResponsiveness.Click += new System.EventHandler(this.btnResponsiveness_Click);
            // 
            // btnNetworkThrottling
            // 
            this.btnNetworkThrottling.Location = new System.Drawing.Point(275, 25);
            this.btnNetworkThrottling.Name = "btnNetworkThrottling";
            this.btnNetworkThrottling.Size = new System.Drawing.Size(230, 50);
            this.btnNetworkThrottling.TabIndex = 12;
            this.btnNetworkThrottling.Text = "Desativar Limitação de Rede";
            this.btnNetworkThrottling.UseVisualStyleBackColor = true;
            this.btnNetworkThrottling.Click += new System.EventHandler(this.btnNetworkThrottling_Click);
            // 
            // btnHighPriority
            // 
            this.btnHighPriority.Location = new System.Drawing.Point(39, 249);
            this.btnHighPriority.Name = "btnHighPriority";
            this.btnHighPriority.Size = new System.Drawing.Size(230, 50);
            this.btnHighPriority.TabIndex = 11;
            this.btnHighPriority.Text = "Alta Prioridade em primeiro plano";
            this.btnHighPriority.UseVisualStyleBackColor = true;
            this.btnHighPriority.Click += new System.EventHandler(this.btnHighPriority_Click);
            // 
            // btnReduceAudioLatency
            // 
            this.btnReduceAudioLatency.Location = new System.Drawing.Point(39, 193);
            this.btnReduceAudioLatency.Name = "btnReduceAudioLatency";
            this.btnReduceAudioLatency.Size = new System.Drawing.Size(230, 50);
            this.btnReduceAudioLatency.TabIndex = 10;
            this.btnReduceAudioLatency.Text = "Reduzir Latência de Áudio";
            this.btnReduceAudioLatency.UseVisualStyleBackColor = true;
            this.btnReduceAudioLatency.Click += new System.EventHandler(this.btnReduceAudioLatency_Click);
            // 
            // btnMassGrave
            // 
            this.btnMassGrave.Location = new System.Drawing.Point(39, 25);
            this.btnMassGrave.Name = "btnMassGrave";
            this.btnMassGrave.Size = new System.Drawing.Size(230, 50);
            this.btnMassGrave.TabIndex = 8;
            this.btnMassGrave.Text = "Abrir MassGrave (MAS)";
            this.btnMassGrave.UseVisualStyleBackColor = true;
            this.btnMassGrave.Click += new System.EventHandler(this.btnMassGrave_Click);
            // 
            // btnUltimatePerformance
            // 
            this.btnUltimatePerformance.Location = new System.Drawing.Point(39, 81);
            this.btnUltimatePerformance.Name = "btnUltimatePerformance";
            this.btnUltimatePerformance.Size = new System.Drawing.Size(230, 50);
            this.btnUltimatePerformance.TabIndex = 9;
            this.btnUltimatePerformance.Text = "Desempenho Máximo";
            this.btnUltimatePerformance.UseVisualStyleBackColor = true;
            this.btnUltimatePerformance.Click += new System.EventHandler(this.btnUltimatePerformance_Click);
            // 
            // btnReboot
            // 
            this.btnReboot.Location = new System.Drawing.Point(631, 401);
            this.btnReboot.Name = "btnReboot";
            this.btnReboot.Size = new System.Drawing.Size(196, 50);
            this.btnReboot.TabIndex = 15;
            this.btnReboot.Text = "Reiniciar PC";
            this.btnReboot.UseVisualStyleBackColor = true;
            this.btnReboot.Click += new System.EventHandler(this.btnReboot_Click);
            // 
            // labelSign
            // 
            this.labelSign.AutoSize = true;
            this.labelSign.Location = new System.Drawing.Point(776, 492);
            this.labelSign.Name = "labelSign";
            this.labelSign.Size = new System.Drawing.Size(88, 16);
            this.labelSign.TabIndex = 16;
            this.labelSign.Text = "Gabriel Maier";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(301, 400);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 16);
            this.lblStatus.TabIndex = 17;
            this.lblStatus.Text = "Status";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(304, 422);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(308, 29);
            this.progressBar.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 511);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.labelSign);
            this.Controls.Add(this.btnReboot);
            this.Controls.Add(this.windowsGB);
            this.Controls.Add(this.NVIDIAGB);
            this.Controls.Add(this.AMDGB);
            this.Controls.Add(this.postFormatGB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Otimizador de PC - by gMaier";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.postFormatGB.ResumeLayout(false);
            this.AMDGB.ResumeLayout(false);
            this.NVIDIAGB.ResumeLayout(false);
            this.windowsGB.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox postFormatGB;
        private System.Windows.Forms.Button btnInstallProg;
        private System.Windows.Forms.GroupBox AMDGB;
        private System.Windows.Forms.Button btnAMDDisableMPO;
        private System.Windows.Forms.Button btnLinkDriverAMD;
        private System.Windows.Forms.GroupBox NVIDIAGB;
        private System.Windows.Forms.Button btnLinkDriverNVIDIA;
        private System.Windows.Forms.Button btnAMDDisableDXNAVI;
        private System.Windows.Forms.GroupBox windowsGB;
        private System.Windows.Forms.Button btnUltimatePerformance;
        private System.Windows.Forms.Button btnMassGrave;
        private System.Windows.Forms.Button btnNetworkThrottling;
        private System.Windows.Forms.Button btnHighPriority;
        private System.Windows.Forms.Button btnReduceAudioLatency;
        private System.Windows.Forms.Button btnResponsiveness;
        private System.Windows.Forms.Button btnAMDSlimmer;
        private System.Windows.Forms.Button btnNVIDIACleanstall;
        private System.Windows.Forms.Button btnPowerPlanSettings;
        private System.Windows.Forms.Button btnReboot;
        private System.Windows.Forms.Button btnDisableSuperFetch;
        private System.Windows.Forms.Button btnExplorerFolderType;
        private System.Windows.Forms.Label labelSign;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

