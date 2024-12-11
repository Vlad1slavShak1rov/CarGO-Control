namespace CarGO_Control.Windows
{
    partial class DriverMaps
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MapPanel = new Panel();
            CityDepartBox = new TextBox();
            CityArrivalBox = new TextBox();
            DistanceLabel = new Label();
            BackButon = new Button();
            LoadBar = new ProgressBar();
            MapPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MapPanel
            // 
            MapPanel.Controls.Add(LoadBar);
            MapPanel.Location = new Point(12, 81);
            MapPanel.Name = "MapPanel";
            MapPanel.Size = new Size(1112, 456);
            MapPanel.TabIndex = 0;
            // 
            // CityDepartBox
            // 
            CityDepartBox.Location = new Point(295, 31);
            CityDepartBox.Name = "CityDepartBox";
            CityDepartBox.Size = new Size(185, 27);
            CityDepartBox.TabIndex = 1;
            // 
            // CityArrivalBox
            // 
            CityArrivalBox.Location = new Point(520, 31);
            CityArrivalBox.Name = "CityArrivalBox";
            CityArrivalBox.Size = new Size(188, 27);
            CityArrivalBox.TabIndex = 2;
            // 
            // DistanceLabel
            // 
            DistanceLabel.AutoSize = true;
            DistanceLabel.Location = new Point(765, 34);
            DistanceLabel.Name = "DistanceLabel";
            DistanceLabel.Size = new Size(50, 20);
            DistanceLabel.TabIndex = 3;
            DistanceLabel.Text = "label1";
            // 
            // BackButon
            // 
            BackButon.Location = new Point(26, 25);
            BackButon.Name = "BackButon";
            BackButon.Size = new Size(94, 29);
            BackButon.TabIndex = 4;
            BackButon.Text = "Назад";
            BackButon.UseVisualStyleBackColor = true;
            BackButon.Click += BackButon_Click;
            // 
            // LoadBar
            // 
            LoadBar.Location = new Point(386, 169);
            LoadBar.Name = "LoadBar";
            LoadBar.Size = new Size(367, 33);
            LoadBar.TabIndex = 0;
            // 
            // DriverMaps
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1136, 569);
            Controls.Add(BackButon);
            Controls.Add(DistanceLabel);
            Controls.Add(CityArrivalBox);
            Controls.Add(CityDepartBox);
            Controls.Add(MapPanel);
            Name = "DriverMaps";
            Text = "DriverMaps";
            Load += DriverMaps_Load;
            MapPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel MapPanel;
        private TextBox CityDepartBox;
        private TextBox CityArrivalBox;
        private Label DistanceLabel;
        private Button BackButon;
        private ProgressBar LoadBar;
    }
}