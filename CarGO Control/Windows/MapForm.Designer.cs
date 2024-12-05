namespace CarGO_Control.Windows
{
    partial class MapForm
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
            SearchButton = new Button();
            SaveButton = new Button();
            ToBox = new TextBox();
            FromBox = new TextBox();
            SearchBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            LoadProgressBar = new ProgressBar();
            MapPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MapPanel
            // 
            MapPanel.Controls.Add(LoadProgressBar);
            MapPanel.Location = new Point(337, 12);
            MapPanel.Name = "MapPanel";
            MapPanel.Size = new Size(758, 550);
            MapPanel.TabIndex = 0;
            // 
            // SearchButton
            // 
            SearchButton.Location = new Point(101, 135);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new Size(94, 29);
            SearchButton.TabIndex = 1;
            SearchButton.Text = "Поиск";
            SearchButton.UseVisualStyleBackColor = true;
            SearchButton.Click += SearchButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(101, 405);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(94, 29);
            SaveButton.TabIndex = 2;
            SaveButton.Text = "Сохранить";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // ToBox
            // 
            ToBox.Location = new Point(57, 355);
            ToBox.Name = "ToBox";
            ToBox.Size = new Size(188, 27);
            ToBox.TabIndex = 3;
            // 
            // FromBox
            // 
            FromBox.Location = new Point(57, 264);
            FromBox.Name = "FromBox";
            FromBox.Size = new Size(188, 27);
            FromBox.TabIndex = 4;
            // 
            // SearchBox
            // 
            SearchBox.Location = new Point(64, 81);
            SearchBox.Name = "SearchBox";
            SearchBox.Size = new Size(181, 27);
            SearchBox.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(124, 42);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 6;
            label1.Text = "Поиск";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(120, 231);
            label2.Name = "label2";
            label2.Size = new Size(56, 20);
            label2.TabIndex = 7;
            label2.Text = "Откуда";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(124, 319);
            label3.Name = "label3";
            label3.Size = new Size(41, 20);
            label3.TabIndex = 8;
            label3.Text = "Куда";
            // 
            // LoadProgressBar
            // 
            LoadProgressBar.Location = new Point(251, 210);
            LoadProgressBar.Name = "LoadProgressBar";
            LoadProgressBar.Size = new Size(280, 29);
            LoadProgressBar.TabIndex = 0;
            LoadProgressBar.Visible = false;
            // 
            // MapForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1099, 566);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(SearchBox);
            Controls.Add(FromBox);
            Controls.Add(ToBox);
            Controls.Add(SaveButton);
            Controls.Add(SearchButton);
            Controls.Add(MapPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MapForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MapForm";
            MapPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel MapPanel;
        private Button SearchButton;
        private Button SaveButton;
        private TextBox ToBox;
        private TextBox FromBox;
        private TextBox SearchBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private ProgressBar LoadProgressBar;
    }
}