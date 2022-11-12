﻿
using System;

namespace Chat.UI
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
            this.SendButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.inputMessageField = new System.Windows.Forms.RichTextBox();
            this.chatWindow = new System.Windows.Forms.RichTextBox();
            this.ActiveUsers = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // SendButton
            // 
            this.SendButton.BackColor = System.Drawing.Color.White;
            this.SendButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SendButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SendButton.FlatAppearance.BorderSize = 2;
            this.SendButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SendButton.Font = new System.Drawing.Font("Corbel", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SendButton.ForeColor = System.Drawing.Color.DimGray;
            this.SendButton.Location = new System.Drawing.Point(746, 412);
            this.SendButton.Margin = new System.Windows.Forms.Padding(0);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(114, 60);
            this.SendButton.TabIndex = 0;
            this.SendButton.Text = "➥";
            this.SendButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.SendButton.UseVisualStyleBackColor = false;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // inputMessageField
            // 
            this.inputMessageField.BackColor = System.Drawing.Color.White;
            this.inputMessageField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inputMessageField.Font = new System.Drawing.Font("Corbel", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.inputMessageField.ForeColor = System.Drawing.Color.Black;
            this.inputMessageField.Location = new System.Drawing.Point(2, 412);
            this.inputMessageField.Margin = new System.Windows.Forms.Padding(2);
            this.inputMessageField.Name = "inputMessageField";
            this.inputMessageField.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.inputMessageField.Size = new System.Drawing.Size(742, 60);
            this.inputMessageField.TabIndex = 1;
            this.inputMessageField.Text = "";
            // 
            // chatWindow
            // 
            this.chatWindow.BackColor = System.Drawing.Color.Gainsboro;
            this.chatWindow.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chatWindow.Location = new System.Drawing.Point(15, 15);
            this.chatWindow.Margin = new System.Windows.Forms.Padding(4);
            this.chatWindow.Name = "chatWindow";
            this.chatWindow.ReadOnly = true;
            this.chatWindow.Size = new System.Drawing.Size(826, 390);
            this.chatWindow.TabIndex = 3;
            this.chatWindow.Text = "";
            this.chatWindow.TextChanged += new System.EventHandler(this.chatWindow_TextChanged);
            // 
            // ActiveUsers
            // 
            this.ActiveUsers.BackColor = System.Drawing.Color.DimGray;
            this.ActiveUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ActiveUsers.Font = new System.Drawing.Font("Corbel", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ActiveUsers.FormattingEnabled = true;
            this.ActiveUsers.ItemHeight = 27;
            this.ActiveUsers.Location = new System.Drawing.Point(843, 15);
            this.ActiveUsers.Name = "ActiveUsers";
            this.ActiveUsers.Size = new System.Drawing.Size(245, 380);
            this.ActiveUsers.TabIndex = 4;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(140)))), ((int)(((byte)(158)))));
            this.ClientSize = new System.Drawing.Size(1088, 471);
            this.Controls.Add(this.ActiveUsers);
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
        private System.Windows.Forms.ListBox ActiveUsers;
    }
}

