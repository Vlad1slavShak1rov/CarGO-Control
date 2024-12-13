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
            LoadBar = new ProgressBar();
            CityDepartBox = new TextBox();
            CityArrivalBox = new TextBox();
            DistanceLabel = new Label();
            BackButon = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ContentBox = new TextBox();
            TypeContentBox = new TextBox();
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
            // LoadBar
            // 
            LoadBar.Location = new Point(386, 169);
            LoadBar.Name = "LoadBar";
            LoadBar.Size = new Size(367, 33);
            LoadBar.TabIndex = 0;
            // 
            // CityDepartBox
            // 
            CityDepartBox.Location = new Point(247, 34);
            CityDepartBox.Name = "CityDepartBox";
            CityDepartBox.ReadOnly = true;
            CityDepartBox.Size = new Size(185, 27);
            CityDepartBox.TabIndex = 1;
            // 
            // CityArrivalBox
            // 
            CityArrivalBox.Location = new Point(471, 33);
            CityArrivalBox.Name = "CityArrivalBox";
            CityArrivalBox.ReadOnly = true;
            CityArrivalBox.Size = new Size(188, 27);
            CityArrivalBox.TabIndex = 2;
            // 
            // DistanceLabel
            // 
            DistanceLabel.AutoSize = true;
            DistanceLabel.Location = new Point(764, 10);
            DistanceLabel.Name = "DistanceLabel";
            DistanceLabel.Size = new Size(39, 20);
            DistanceLabel.TabIndex = 3;
            DistanceLabel.Text = "Груз";
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(304, 9);
            label1.Name = "label1";
            label1.Size = new Size(56, 20);
            label1.TabIndex = 5;
            label1.Text = "Откуда";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(536, 10);
            label2.Name = "label2";
            label2.Size = new Size(41, 20);
            label2.TabIndex = 6;
            label2.Text = "Куда";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(949, 10);
            label3.Name = "label3";
            label3.Size = new Size(76, 20);
            label3.TabIndex = 7;
            label3.Text = "Тип груза";
            // 
            // ContentBox
            // 
            ContentBox.Location = new Point(696, 34);
            ContentBox.Name = "ContentBox";
            ContentBox.ReadOnly = true;
            ContentBox.Size = new Size(175, 27);
            ContentBox.TabIndex = 8;
            // 
            // TypeContentBox
            // 
            TypeContentBox.Location = new Point(899, 34);
            TypeContentBox.Name = "TypeContentBox";
            TypeContentBox.ReadOnly = true;
            TypeContentBox.Size = new Size(175, 27);
            TypeContentBox.TabIndex = 9;
            // 
            // DriverMaps
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1136, 569);
            Controls.Add(TypeContentBox);
            Controls.Add(ContentBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(BackButon);
            Controls.Add(DistanceLabel);
            Controls.Add(CityArrivalBox);
            Controls.Add(CityDepartBox);
            Controls.Add(MapPanel);
            Name = "DriverMaps";
            StartPosition = FormStartPosition.CenterScreen;
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
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox ContentBox;
        private TextBox TypeContentBox;
    }
}