namespace Tetris
{
    partial class TetrisForm
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
            this.top_layout = new System.Windows.Forms.TableLayoutPanel();
            this.top_right_lay_out = new System.Windows.Forms.TableLayoutPanel();
            this.top_layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // top_layout
            // 
            this.top_layout.ColumnCount = 2;
            this.top_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.top_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.top_layout.Controls.Add(this.top_right_lay_out, 1, 0);
            this.top_layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.top_layout.Location = new System.Drawing.Point(0, 0);
            this.top_layout.Name = "top_layout";
            this.top_layout.RowCount = 1;
            this.top_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.top_layout.Size = new System.Drawing.Size(327, 328);
            this.top_layout.TabIndex = 0;
            // 
            // top_right_lay_out
            // 
            this.top_right_lay_out.ColumnCount = 1;
            this.top_right_lay_out.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.top_right_lay_out.Dock = System.Windows.Forms.DockStyle.Fill;
            this.top_right_lay_out.Location = new System.Drawing.Point(5, 30);
            this.top_right_lay_out.Margin = new System.Windows.Forms.Padding(5, 30, 5, 30);
            this.top_right_lay_out.Name = "top_right_lay_out";
            this.top_right_lay_out.RowCount = 7;
            this.top_right_lay_out.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.top_right_lay_out.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.top_right_lay_out.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.top_right_lay_out.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.top_right_lay_out.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.top_right_lay_out.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.top_right_lay_out.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.top_right_lay_out.Size = new System.Drawing.Size(317, 268);
            this.top_right_lay_out.TabIndex = 0;
            // 
            // TetrisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 328);
            this.Controls.Add(this.top_layout);
            this.Name = "TetrisForm";
            this.Text = "Tetris";
            this.top_layout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel top_layout;
        private System.Windows.Forms.TableLayoutPanel top_right_lay_out;
    }
}

