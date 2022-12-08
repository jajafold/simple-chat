namespace Chat.UI.RoomSelection
{
    partial class RoomCreation
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
            this._lRoomName = new System.Windows.Forms.Label();
            this._tbRoomName = new System.Windows.Forms.TextBox();
            this._cbIsPasswordSet = new System.Windows.Forms.CheckBox();
            this._tbPassword = new System.Windows.Forms.TextBox();
            this._lRoomCapacity = new System.Windows.Forms.Label();
            this._lRoomCapacityExtras = new System.Windows.Forms.Label();
            this._nudRoomCapacity = new System.Windows.Forms.NumericUpDown();
            this._bCreate = new System.Windows.Forms.Button();
            this._bCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._nudRoomCapacity)).BeginInit();
            this.SuspendLayout();
            // 
            // _lRoomName
            // 
            this._lRoomName.AutoSize = true;
            this._lRoomName.ForeColor = System.Drawing.Color.Black;
            this._lRoomName.Location = new System.Drawing.Point(33, 32);
            this._lRoomName.Name = "_lRoomName";
            this._lRoomName.Size = new System.Drawing.Size(142, 20);
            this._lRoomName.TabIndex = 0;
            this._lRoomName.Text = "Название комнаты";
            // 
            // _tbRoomName
            // 
            this._tbRoomName.BackColor = System.Drawing.Color.WhiteSmoke;
            this._tbRoomName.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._tbRoomName.Location = new System.Drawing.Point(33, 55);
            this._tbRoomName.Name = "_tbRoomName";
            this._tbRoomName.Size = new System.Drawing.Size(325, 31);
            this._tbRoomName.TabIndex = 1;
            // 
            // _cbIsPasswordSet
            // 
            this._cbIsPasswordSet.AutoSize = true;
            this._cbIsPasswordSet.BackColor = System.Drawing.Color.Transparent;
            this._cbIsPasswordSet.ForeColor = System.Drawing.Color.Black;
            this._cbIsPasswordSet.Location = new System.Drawing.Point(33, 150);
            this._cbIsPasswordSet.Name = "_cbIsPasswordSet";
            this._cbIsPasswordSet.Size = new System.Drawing.Size(184, 24);
            this._cbIsPasswordSet.TabIndex = 2;
            this._cbIsPasswordSet.Text = "Использовать пароль";
            this._cbIsPasswordSet.UseVisualStyleBackColor = false;
            // 
            // _tbPassword
            // 
            this._tbPassword.BackColor = System.Drawing.Color.WhiteSmoke;
            this._tbPassword.Enabled = false;
            this._tbPassword.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._tbPassword.Location = new System.Drawing.Point(33, 180);
            this._tbPassword.Name = "_tbPassword";
            this._tbPassword.Size = new System.Drawing.Size(325, 31);
            this._tbPassword.TabIndex = 3;
            // 
            // _lRoomCapacity
            // 
            this._lRoomCapacity.AutoSize = true;
            this._lRoomCapacity.BackColor = System.Drawing.Color.Transparent;
            this._lRoomCapacity.ForeColor = System.Drawing.Color.Black;
            this._lRoomCapacity.Location = new System.Drawing.Point(33, 280);
            this._lRoomCapacity.Name = "_lRoomCapacity";
            this._lRoomCapacity.Size = new System.Drawing.Size(172, 20);
            this._lRoomCapacity.TabIndex = 4;
            this._lRoomCapacity.Text = "Количество участников";
            // 
            // _lRoomCapacityExtras
            // 
            this._lRoomCapacityExtras.AutoSize = true;
            this._lRoomCapacityExtras.BackColor = System.Drawing.Color.Transparent;
            this._lRoomCapacityExtras.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._lRoomCapacityExtras.ForeColor = System.Drawing.Color.Gray;
            this._lRoomCapacityExtras.Location = new System.Drawing.Point(67, 300);
            this._lRoomCapacityExtras.Name = "_lRoomCapacityExtras";
            this._lRoomCapacityExtras.Size = new System.Drawing.Size(138, 17);
            this._lRoomCapacityExtras.TabIndex = 5;
            this._lRoomCapacityExtras.Text = "0 — без ограничений";
            // 
            // _nudRoomCapacity
            // 
            this._nudRoomCapacity.BackColor = System.Drawing.Color.WhiteSmoke;
            this._nudRoomCapacity.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._nudRoomCapacity.ForeColor = System.Drawing.Color.Black;
            this._nudRoomCapacity.Location = new System.Drawing.Point(251, 287);
            this._nudRoomCapacity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._nudRoomCapacity.Name = "_nudRoomCapacity";
            this._nudRoomCapacity.Size = new System.Drawing.Size(107, 30);
            this._nudRoomCapacity.TabIndex = 6;
            // 
            // _bCreate
            // 
            this._bCreate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._bCreate.ForeColor = System.Drawing.Color.Black;
            this._bCreate.Location = new System.Drawing.Point(183, 396);
            this._bCreate.Name = "_bCreate";
            this._bCreate.Size = new System.Drawing.Size(94, 30);
            this._bCreate.TabIndex = 7;
            this._bCreate.Text = "Создать";
            this._bCreate.UseVisualStyleBackColor = true;
            // 
            // _bCancel
            // 
            this._bCancel.BackColor = System.Drawing.Color.Transparent;
            this._bCancel.ForeColor = System.Drawing.Color.Black;
            this._bCancel.Location = new System.Drawing.Point(294, 396);
            this._bCancel.Name = "_bCancel";
            this._bCancel.Size = new System.Drawing.Size(94, 29);
            this._bCancel.TabIndex = 8;
            this._bCancel.Text = "Отмена";
            this._bCancel.UseVisualStyleBackColor = false;
            // 
            // RoomCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this._bCancel);
            this.Controls.Add(this._bCreate);
            this.Controls.Add(this._nudRoomCapacity);
            this.Controls.Add(this._lRoomCapacityExtras);
            this.Controls.Add(this._lRoomCapacity);
            this.Controls.Add(this._tbPassword);
            this.Controls.Add(this._cbIsPasswordSet);
            this.Controls.Add(this._tbRoomName);
            this.Controls.Add(this._lRoomName);
            this.Name = "RoomCreation";
            this.Text = "Создание комнаты";
            ((System.ComponentModel.ISupportInitialize)(this._nudRoomCapacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lRoomName;
        private System.Windows.Forms.TextBox _tbRoomName;
        private System.Windows.Forms.CheckBox _cbIsPasswordSet;
        private System.Windows.Forms.TextBox _tbPassword;
        private System.Windows.Forms.Label _lRoomCapacity;
        private System.Windows.Forms.Label _lRoomCapacityExtras;
        private System.Windows.Forms.NumericUpDown _nudRoomCapacity;
        private System.Windows.Forms.Button _bCreate;
        private System.Windows.Forms.Button _bCancel;
    }
}