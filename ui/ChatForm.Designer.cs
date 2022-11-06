
namespace chat
{
    partial class ChatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.SendBotton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.String = new System.Windows.Forms.RichTextBox();
            this.massages = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SendBotton
            // 
            this.SendBotton.BackColor = System.Drawing.Color.SteelBlue;
            this.SendBotton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SendBotton.BackgroundImage")));
            this.SendBotton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SendBotton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.SendBotton.FlatAppearance.BorderSize = 0;
            this.SendBotton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SendBotton.Location = new System.Drawing.Point(742, 412);
            this.SendBotton.Margin = new System.Windows.Forms.Padding(0);
            this.SendBotton.Name = "SendBotton";
            this.SendBotton.Size = new System.Drawing.Size(100, 60);
            this.SendBotton.TabIndex = 0;
            this.SendBotton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.SendBotton.UseVisualStyleBackColor = false;
            this.SendBotton.Click += new System.EventHandler(this.button1_Click);
            // 
            // String
            // 
            this.String.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.String.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.String.Location = new System.Drawing.Point(2, 412);
            this.String.Name = "String";
            this.String.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.String.Size = new System.Drawing.Size(742, 60);
            this.String.TabIndex = 1;
            this.String.Text = "";
            // 
            // massages
            // 
            this.massages.AutoSize = true;
            this.massages.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.massages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.massages.Location = new System.Drawing.Point(2, 3);
            this.massages.Name = "massages";
            this.massages.Size = new System.Drawing.Size(0, 29);
            this.massages.TabIndex = 2;
            this.massages.Click += new System.EventHandler(this.label1_Click);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(140)))), ((int)(((byte)(158)))));
            this.ClientSize = new System.Drawing.Size(840, 472);
            this.Controls.Add(this.massages);
            this.Controls.Add(this.String);
            this.Controls.Add(this.SendBotton);
            this.Name = "ChatForm";
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SendBotton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.RichTextBox String;
        private System.Windows.Forms.Label massages;
    }
}

