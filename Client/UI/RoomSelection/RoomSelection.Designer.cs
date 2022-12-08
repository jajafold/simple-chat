namespace Chat.UI.RoomSelection
{
    partial class RoomSelection
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
            this._buttonConnect = new System.Windows.Forms.Button();
            this._roomSelectionTable = new System.Windows.Forms.DataGridView();
            this._DEBUG_selected = new System.Windows.Forms.Label();
            this._buttonRoomCreation = new System.Windows.Forms.Button();
            this.roomName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomPermission = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomMembersCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._roomSelectionTable)).BeginInit();
            this.SuspendLayout();
            // 
            // _buttonConnect
            // 
            this._buttonConnect.Location = new System.Drawing.Point(12, 409);
            this._buttonConnect.Name = "_buttonConnect";
            this._buttonConnect.Size = new System.Drawing.Size(140, 29);
            this._buttonConnect.TabIndex = 1;
            this._buttonConnect.Text = "Подключиться";
            this._buttonConnect.UseVisualStyleBackColor = true;
            // 
            // _roomSelectionTable
            // 
            this._roomSelectionTable.AllowUserToAddRows = false;
            this._roomSelectionTable.AllowUserToDeleteRows = false;
            this._roomSelectionTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this._roomSelectionTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._roomSelectionTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.roomName,
            this.roomPermission,
            this.roomMembersCount,
            this.roomGuid});
            this._roomSelectionTable.Location = new System.Drawing.Point(12, 12);
            this._roomSelectionTable.Name = "_roomSelectionTable";
            this._roomSelectionTable.ReadOnly = true;
            this._roomSelectionTable.RowHeadersWidth = 51;
            this._roomSelectionTable.RowTemplate.Height = 29;
            this._roomSelectionTable.Size = new System.Drawing.Size(536, 376);
            this._roomSelectionTable.TabIndex = 2;
            // 
            // _DEBUG_selected
            // 
            this._DEBUG_selected.AutoSize = true;
            this._DEBUG_selected.Location = new System.Drawing.Point(640, 134);
            this._DEBUG_selected.Name = "_DEBUG_selected";
            this._DEBUG_selected.Size = new System.Drawing.Size(50, 20);
            this._DEBUG_selected.TabIndex = 3;
            this._DEBUG_selected.Text = "label1";
            // 
            // _buttonRoomCreation
            // 
            this._buttonRoomCreation.Location = new System.Drawing.Point(394, 409);
            this._buttonRoomCreation.Name = "_buttonRoomCreation";
            this._buttonRoomCreation.Size = new System.Drawing.Size(154, 29);
            this._buttonRoomCreation.TabIndex = 4;
            this._buttonRoomCreation.Text = "Создать комнату";
            this._buttonRoomCreation.UseVisualStyleBackColor = true;
            // 
            // roomName
            // 
            this.roomName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.roomName.HeaderText = "Название";
            this.roomName.MinimumWidth = 50;
            this.roomName.Name = "roomName";
            this.roomName.ReadOnly = true;
            this.roomName.Width = 106;
            // 
            // roomPermission
            // 
            this.roomPermission.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.roomPermission.HeaderText = "Приватность";
            this.roomPermission.MinimumWidth = 6;
            this.roomPermission.Name = "roomPermission";
            this.roomPermission.ReadOnly = true;
            this.roomPermission.Width = 128;
            // 
            // roomMembersCount
            // 
            this.roomMembersCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.roomMembersCount.HeaderText = "Участники";
            this.roomMembersCount.MinimumWidth = 6;
            this.roomMembersCount.Name = "roomMembersCount";
            this.roomMembersCount.ReadOnly = true;
            this.roomMembersCount.Width = 110;
            // 
            // roomGuid
            // 
            this.roomGuid.HeaderText = "ID комнаты";
            this.roomGuid.MinimumWidth = 6;
            this.roomGuid.Name = "roomGuid";
            this.roomGuid.ReadOnly = true;
            this.roomGuid.Visible = false;
            this.roomGuid.Width = 125;
            // 
            // RoomSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._buttonRoomCreation);
            this.Controls.Add(this._DEBUG_selected);
            this.Controls.Add(this._roomSelectionTable);
            this.Controls.Add(this._buttonConnect);
            this.Name = "RoomSelection";
            this.Text = "Выбор комнаты";
            ((System.ComponentModel.ISupportInitialize)(this._roomSelectionTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button _buttonConnect;
        private System.Windows.Forms.DataGridView _roomSelectionTable;
        private System.Windows.Forms.Label _DEBUG_selected;
        private System.Windows.Forms.Button _buttonRoomCreation;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomName;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomPermission;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomMembersCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomGuid;
    }
}