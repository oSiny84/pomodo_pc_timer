namespace PomodoTimer
{
    partial class TimerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimerForm));
            textBox1 = new TextBox();
            button_Reset = new Button();
            checkBox_Top = new CheckBox();
            checkBox_TimerHolding = new CheckBox();
            button_min_up = new Button();
            button_min_down = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(34, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(78, 23);
            textBox1.TabIndex = 0;
            // 
            // button_Reset
            // 
            button_Reset.Location = new Point(209, 11);
            button_Reset.Name = "button_Reset";
            button_Reset.Size = new Size(52, 23);
            button_Reset.TabIndex = 1;
            button_Reset.Text = "Reset";
            button_Reset.UseVisualStyleBackColor = true;
            button_Reset.Click += button1_Click;
            // 
            // checkBox_Top
            // 
            checkBox_Top.AutoSize = true;
            checkBox_Top.Location = new Point(1, 15);
            checkBox_Top.Name = "checkBox_Top";
            checkBox_Top.Size = new Size(32, 19);
            checkBox_Top.TabIndex = 2;
            checkBox_Top.Text = "T";
            checkBox_Top.UseVisualStyleBackColor = true;
            checkBox_Top.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox_TimerHolding
            // 
            checkBox_TimerHolding.AutoSize = true;
            checkBox_TimerHolding.Location = new Point(113, 15);
            checkBox_TimerHolding.Name = "checkBox_TimerHolding";
            checkBox_TimerHolding.Size = new Size(35, 19);
            checkBox_TimerHolding.TabIndex = 2;
            checkBox_TimerHolding.Text = "H";
            checkBox_TimerHolding.UseVisualStyleBackColor = true;
            // 
            // button_min_up
            // 
            button_min_up.Location = new Point(148, 12);
            button_min_up.Name = "button_min_up";
            button_min_up.Size = new Size(27, 23);
            button_min_up.TabIndex = 1;
            button_min_up.Text = "U";
            button_min_up.UseVisualStyleBackColor = true;
            button_min_up.Click += button_min_up_Click;
            // 
            // button_min_down
            // 
            button_min_down.Location = new Point(176, 12);
            button_min_down.Name = "button_min_down";
            button_min_down.Size = new Size(27, 23);
            button_min_down.TabIndex = 1;
            button_min_down.Text = "D";
            button_min_down.UseVisualStyleBackColor = true;
            button_min_down.Click += button_min_down_Click;
            // 
            // TimerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(269, 42);
            Controls.Add(checkBox_TimerHolding);
            Controls.Add(checkBox_Top);
            Controls.Add(button_min_down);
            Controls.Add(button_min_up);
            Controls.Add(button_Reset);
            Controls.Add(textBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "TimerForm";
            Text = "timer";
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button_Reset;
        private CheckBox checkBox_Top;
        private CheckBox checkBox_TimerHolding;
        private Button button2;
        private Button button3;
        private Button button_min_up;
        private Button button_min_down;
    }
}