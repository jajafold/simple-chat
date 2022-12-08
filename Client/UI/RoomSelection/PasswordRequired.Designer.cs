namespace Chat.UI.RoomSelection
{
    partial class PasswordRequired
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
            this._lPasswordRequired = new System.Windows.Forms.Label();
            this._tbPassword = new System.Windows.Forms.TextBox();
            this._bConfirm = new System.Windows.Forms.Button();
            this._bCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _lPasswordRequired
            // 
            this._lPasswordRequired.AutoSize = true;
            this._lPasswordRequired.BackColor = System.Drawing.Color.Transparent;
            this._lPasswordRequired.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._lPasswordRequired.Location = new System.Drawing.Point(29, 29);
            this._lPasswordRequired.Name = "_lPasswordRequired";
            this._lPasswordRequired.Size = new System.Drawing.Size(441, 25);
            this._lPasswordRequired.TabIndex = 0;
            this._lPasswordRequired.Text = "Эта закрытая комната. Пожалуйста, введите пароль.";
            // 
            // _tbPassword
            // 
            this._tbPassword.BackColor = System.Drawing.Color.WhiteSmoke;
            this._tbPassword.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._tbPassword.Location = new System.Drawing.Point(133, 87);
            this._tbPassword.Name = "_tbPassword";
            this._tbPassword.Size = new System.Drawing.Size(240, 31);
            this._tbPassword.TabIndex = 1;
            // 
            // _bConfirm
            // 
            this._bConfirm.BackColor = System.Drawing.Color.Transparent;
            this._bConfirm.Location = new System.Drawing.Point(303, 155);
            this._bConfirm.Name = "_bConfirm";
            this._bConfirm.Size = new System.Drawing.Size(94, 29);
            this._bConfirm.TabIndex = 2;
            this._bConfirm.Text = "Ок";
            this._bConfirm.UseVisualStyleBackColor = false;
            // 
            // _bCancel
            // 
            this._bCancel.BackColor = System.Drawing.Color.Transparent;
            this._bCancel.Location = new System.Drawing.Point(414, 155);
            this._bCancel.Name = "_bCancel";
            this._bCancel.Size = new System.Drawing.Size(94, 29);
            this._bCancel.TabIndex = 3;
            this._bCancel.Text = "Отмена";
            this._bCancel.UseVisualStyleBackColor = false;
            // 
            // PasswordRequired
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 196);
            this.Controls.Add(this._bCancel);
            this.Controls.Add(this._bConfirm);
            this.Controls.Add(this._tbPassword);
            this.Controls.Add(this._lPasswordRequired);
            this.Name = "PasswordRequired";
            this.Text = "Безопасность";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lPasswordRequired;
        private System.Windows.Forms.TextBox _tbPassword;
        private System.Windows.Forms.Button _bConfirm;
        private System.Windows.Forms.Button _bCancel;
    }
}