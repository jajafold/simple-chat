
using System;

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
            this.SendButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.inputMessageField = new System.Windows.Forms.RichTextBox();
            this.chatWindow = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // SendButton
            // 
            this.SendButton.BackColor = System.Drawing.Color.SteelBlue;
            this.SendButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SendButton.BackgroundImage")));
            this.SendButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SendButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.SendButton.FlatAppearance.BorderSize = 0;
            this.SendButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SendButton.Location = new System.Drawing.Point(594, 330);
            this.SendButton.Margin = new System.Windows.Forms.Padding(0);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(80, 48);
            this.SendButton.TabIndex = 0;
            this.SendButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.SendButton.UseVisualStyleBackColor = false;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // inputMessageField
            // 
            this.inputMessageField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.inputMessageField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inputMessageField.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.inputMessageField.Location = new System.Drawing.Point(2, 330);
            this.inputMessageField.Margin = new System.Windows.Forms.Padding(2);
            this.inputMessageField.Name = "inputMessageField";
            this.inputMessageField.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.inputMessageField.Size = new System.Drawing.Size(594, 48);
            this.inputMessageField.TabIndex = 1;
            this.inputMessageField.Text = "";
            // 
            // chatWindow
            // 
            this.chatWindow.Location = new System.Drawing.Point(12, 12);
            this.chatWindow.Name = "chatWindow";
            this.chatWindow.ReadOnly = true;
            this.chatWindow.Size = new System.Drawing.Size(662, 313);
            this.chatWindow.TabIndex = 3;
            this.chatWindow.Text = "";
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(140)))), ((int)(((byte)(158)))));
            this.ClientSize = new System.Drawing.Size(688, 377);
            this.Controls.Add(this.chatWindow);
            this.Controls.Add(this.inputMessageField);
            this.Controls.Add(this.SendButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ChatForm";
            this.Text = "Chat";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SendButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.RichTextBox inputMessageField;
        internal System.Windows.Forms.RichTextBox chatWindow;
    }
}

