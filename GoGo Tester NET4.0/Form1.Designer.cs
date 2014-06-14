namespace GoGo_Tester_NET4._0
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTestUrl = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.tbIpRange = new System.Windows.Forms.TextBox();
            this.bAddIpRange = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.cbAutoTest = new System.Windows.Forms.CheckBox();
            this.cbAutoSave = new System.Windows.Forms.CheckBox();
            this.cbShutdown = new System.Windows.Forms.CheckBox();
            this.lTip = new System.Windows.Forms.Label();
            this.tbTestTimeout = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMaxSockets = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTimeout = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.bDelete = new System.Windows.Forms.Button();
            this.bExportSelect = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.bApply = new System.Windows.Forms.Button();
            this.bExportValid = new System.Windows.Forms.Button();
            this.bGaTest = new System.Windows.Forms.Button();
            this.bImport = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.dgvIpData = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cbSslStatus = new System.Windows.Forms.CheckBox();
            this.cbTcp80 = new System.Windows.Forms.CheckBox();
            this.cbPing = new System.Windows.Forms.CheckBox();
            this.cbTcp443 = new System.Windows.Forms.CheckBox();
            this.bTest = new System.Windows.Forms.Button();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIpData)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tbTestUrl, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 381);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(507, 21);
            this.tableLayoutPanel6.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "测试网址：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbTestUrl
            // 
            this.tbTestUrl.Location = new System.Drawing.Point(71, 0);
            this.tbTestUrl.Margin = new System.Windows.Forms.Padding(0);
            this.tbTestUrl.Name = "tbTestUrl";
            this.tbTestUrl.Size = new System.Drawing.Size(436, 21);
            this.tbTestUrl.TabIndex = 5;
            this.tbTestUrl.Tag = "用来测试的网址，可以随意指定，但最好是一个网站的主页或一级页面。";
            this.tbTestUrl.Text = "https://www.google.com";
            this.tbTestUrl.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tbIpRange, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.bAddIpRange, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 353);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(507, 22);
            this.tableLayoutPanel5.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 22);
            this.label5.TabIndex = 0;
            this.label5.Text = "添加IP段：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbIpRange
            // 
            this.tbIpRange.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbIpRange.Location = new System.Drawing.Point(71, 0);
            this.tbIpRange.Margin = new System.Windows.Forms.Padding(0);
            this.tbIpRange.Name = "tbIpRange";
            this.tbIpRange.Size = new System.Drawing.Size(397, 21);
            this.tbIpRange.TabIndex = 1;
            this.tbIpRange.Tag = "允许添加IP段，格式 0-255.0-255.0-255.0-255 代表所有IP， 173.0-255.0-255.0-255 代表 173 段所有 IP ，其" +
    "它同理。不同IP段请用逗号\',\'或者分号\';\'分隔（半全角皆可）";
            this.tbIpRange.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // bAddIpRange
            // 
            this.bAddIpRange.AutoSize = true;
            this.bAddIpRange.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bAddIpRange.Location = new System.Drawing.Point(468, 0);
            this.bAddIpRange.Margin = new System.Windows.Forms.Padding(0);
            this.bAddIpRange.Name = "bAddIpRange";
            this.bAddIpRange.Size = new System.Drawing.Size(39, 22);
            this.bAddIpRange.TabIndex = 2;
            this.bAddIpRange.Text = "添加";
            this.bAddIpRange.UseVisualStyleBackColor = true;
            this.bAddIpRange.Click += new System.EventHandler(this.bAddIpRange_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Controls.Add(this.cbAutoTest, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbAutoSave, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbShutdown, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 435);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(507, 22);
            this.tableLayoutPanel4.TabIndex = 14;
            // 
            // cbAutoTest
            // 
            this.cbAutoTest.AutoSize = true;
            this.cbAutoTest.Checked = true;
            this.cbAutoTest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoTest.Location = new System.Drawing.Point(341, 3);
            this.cbAutoTest.Name = "cbAutoTest";
            this.cbAutoTest.Size = new System.Drawing.Size(132, 16);
            this.cbAutoTest.TabIndex = 9;
            this.cbAutoTest.Tag = "导入IP时自动开始测试。";
            this.cbAutoTest.Text = "导入IP自动开始测试";
            this.cbAutoTest.UseVisualStyleBackColor = true;
            this.cbAutoTest.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // cbAutoSave
            // 
            this.cbAutoSave.AutoSize = true;
            this.cbAutoSave.Checked = true;
            this.cbAutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoSave.Location = new System.Drawing.Point(172, 3);
            this.cbAutoSave.Name = "cbAutoSave";
            this.cbAutoSave.Size = new System.Drawing.Size(144, 16);
            this.cbAutoSave.TabIndex = 9;
            this.cbAutoSave.Tag = "在一个测试中如果一个IP通过测试，那么它会被自动保存到\'validips.txt\'文件！单击【GoAgent测试】会清空这个文件。";
            this.cbAutoSave.Text = "测试通过的IP自动保存";
            this.cbAutoSave.UseVisualStyleBackColor = true;
            this.cbAutoSave.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // cbShutdown
            // 
            this.cbShutdown.AutoSize = true;
            this.cbShutdown.Location = new System.Drawing.Point(3, 3);
            this.cbShutdown.Name = "cbShutdown";
            this.cbShutdown.Size = new System.Drawing.Size(144, 16);
            this.cbShutdown.TabIndex = 8;
            this.cbShutdown.Tag = "在所有测试完成后关闭计算机，并把结果导出到程序目录下的\'gogoiplist.txt\'文件。";
            this.cbShutdown.Text = "完成后导出列表并关机";
            this.cbShutdown.UseVisualStyleBackColor = true;
            this.cbShutdown.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // lTip
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lTip, 2);
            this.lTip.Dock = System.Windows.Forms.DockStyle.Top;
            this.lTip.Location = new System.Drawing.Point(3, 460);
            this.lTip.Name = "lTip";
            this.lTip.Padding = new System.Windows.Forms.Padding(3);
            this.lTip.Size = new System.Drawing.Size(724, 40);
            this.lTip.TabIndex = 12;
            this.lTip.Text = " ";
            // 
            // tbTestTimeout
            // 
            this.tbTestTimeout.Location = new System.Drawing.Point(415, 0);
            this.tbTestTimeout.Margin = new System.Windows.Forms.Padding(0);
            this.tbTestTimeout.Name = "tbTestTimeout";
            this.tbTestTimeout.Size = new System.Drawing.Size(92, 21);
            this.tbTestTimeout.TabIndex = 11;
            this.tbTestTimeout.Tag = "GoAgent测试时单个IP的测试时间，超过该时间，当前测试会终止，然后启动下一个测试。亦作为判断获取 SSL Status 超时。";
            this.tbTestTimeout.Text = "6000";
            this.tbTestTimeout.TextChanged += new System.EventHandler(this.tbTestTimeout_TextChanged);
            this.tbTestTimeout.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(350, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 21);
            this.label4.TabIndex = 10;
            this.label4.Text = "测试超时：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbMaxSockets
            // 
            this.tbMaxSockets.Location = new System.Drawing.Point(258, 0);
            this.tbMaxSockets.Margin = new System.Windows.Forms.Padding(0);
            this.tbMaxSockets.Name = "tbMaxSockets";
            this.tbMaxSockets.Size = new System.Drawing.Size(92, 21);
            this.tbMaxSockets.TabIndex = 9;
            this.tbMaxSockets.Tag = "最多同时保持几个Ping线程，一般不用动他，如果你的电脑和网络够好，可以加大。";
            this.tbMaxSockets.Text = "20";
            this.tbMaxSockets.TextChanged += new System.EventHandler(this.tbMaxSockets_TextChanged);
            this.tbMaxSockets.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(157, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "最大Ping线程数：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbTimeout
            // 
            this.tbTimeout.Location = new System.Drawing.Point(65, 0);
            this.tbTimeout.Margin = new System.Windows.Forms.Padding(0);
            this.tbTimeout.Name = "tbTimeout";
            this.tbTimeout.Size = new System.Drawing.Size(92, 21);
            this.tbTimeout.TabIndex = 4;
            this.tbTimeout.Tag = "各种Ping的最大延时。";
            this.tbTimeout.Text = "1000";
            this.tbTimeout.TextChanged += new System.EventHandler(this.tbTimeout_TextChanged);
            this.tbTimeout.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "最大延时：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.bTest, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.bDelete, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.bExportSelect, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.bClear, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.bApply, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.bExportValid, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.bGaTest, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.bImport, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbLog, 0, 8);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(516, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 9;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 5);
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(211, 454);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // bDelete
            // 
            this.bDelete.AutoSize = true;
            this.bDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bDelete.Dock = System.Windows.Forms.DockStyle.Top;
            this.bDelete.Location = new System.Drawing.Point(3, 314);
            this.bDelete.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.bDelete.Name = "bDelete";
            this.bDelete.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.bDelete.Size = new System.Drawing.Size(205, 32);
            this.bDelete.TabIndex = 7;
            this.bDelete.Tag = "如果列表中存在IP a,b,c,d，剪切板中存在IP a,c，那么列表中的 a,c 会被移除，只剩下 b,d 。与【导出选中IP】操作搭配可以完成删除操作。";
            this.bDelete.Text = "排除相同IP";
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            this.bDelete.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // bExportSelect
            // 
            this.bExportSelect.AutoSize = true;
            this.bExportSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bExportSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.bExportSelect.Location = new System.Drawing.Point(3, 270);
            this.bExportSelect.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.bExportSelect.Name = "bExportSelect";
            this.bExportSelect.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.bExportSelect.Size = new System.Drawing.Size(205, 32);
            this.bExportSelect.TabIndex = 6;
            this.bExportSelect.Tag = "把选中的IP按照其排列顺序用\'|\'分隔然后导出到剪切板。";
            this.bExportSelect.Text = "导出选中IP";
            this.bExportSelect.UseVisualStyleBackColor = true;
            this.bExportSelect.Click += new System.EventHandler(this.bExportSelect_Click);
            this.bExportSelect.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // bClear
            // 
            this.bClear.AutoSize = true;
            this.bClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bClear.Dock = System.Windows.Forms.DockStyle.Top;
            this.bClear.Location = new System.Drawing.Point(3, 226);
            this.bClear.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.bClear.Name = "bClear";
            this.bClear.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.bClear.Size = new System.Drawing.Size(205, 32);
            this.bClear.TabIndex = 4;
            this.bClear.Tag = "清除整个IP列表。";
            this.bClear.Text = "清除IP列表";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            this.bClear.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // bApply
            // 
            this.bApply.AutoSize = true;
            this.bApply.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bApply.Dock = System.Windows.Forms.DockStyle.Top;
            this.bApply.Location = new System.Drawing.Point(3, 182);
            this.bApply.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.bApply.Name = "bApply";
            this.bApply.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.bApply.Size = new System.Drawing.Size(205, 32);
            this.bApply.TabIndex = 3;
            this.bApply.Tag = "把SslStatus 或 GoAgent 中标记为 _OK 的IP直接应用到 proxy.ini 文件的 [iplist] 下的 google_hk 和 goog" +
    "le_cn 中。";
            this.bApply.Text = "应用有效IP";
            this.bApply.UseVisualStyleBackColor = true;
            this.bApply.Click += new System.EventHandler(this.bApply_Click);
            this.bApply.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // bExportValid
            // 
            this.bExportValid.AutoSize = true;
            this.bExportValid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bExportValid.Dock = System.Windows.Forms.DockStyle.Top;
            this.bExportValid.Location = new System.Drawing.Point(3, 138);
            this.bExportValid.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.bExportValid.Name = "bExportValid";
            this.bExportValid.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.bExportValid.Size = new System.Drawing.Size(205, 32);
            this.bExportValid.TabIndex = 2;
            this.bExportValid.Tag = "把在GoAgent测试中通过的IP按\'|\'分隔后导出到剪切板。";
            this.bExportValid.Text = "导出有效IP";
            this.bExportValid.UseVisualStyleBackColor = true;
            this.bExportValid.Click += new System.EventHandler(this.bExport_Click);
            this.bExportValid.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // bGaTest
            // 
            this.bGaTest.AutoSize = true;
            this.bGaTest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bGaTest.Dock = System.Windows.Forms.DockStyle.Top;
            this.bGaTest.Location = new System.Drawing.Point(3, 94);
            this.bGaTest.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.bGaTest.Name = "bGaTest";
            this.bGaTest.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.bGaTest.Size = new System.Drawing.Size(205, 32);
            this.bGaTest.TabIndex = 1;
            this.bGaTest.Tag = "（可选）开始调用GoAgent程序模拟请求测试网址，测试Ping通的IP是否在GoAgent中可用。";
            this.bGaTest.Text = "GoAgent测试";
            this.bGaTest.UseVisualStyleBackColor = true;
            this.bGaTest.Click += new System.EventHandler(this.bTest_Click);
            this.bGaTest.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // bImport
            // 
            this.bImport.AutoSize = true;
            this.bImport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bImport.Dock = System.Windows.Forms.DockStyle.Top;
            this.bImport.Location = new System.Drawing.Point(3, 6);
            this.bImport.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.bImport.Name = "bImport";
            this.bImport.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.bImport.Size = new System.Drawing.Size(205, 32);
            this.bImport.TabIndex = 0;
            this.bImport.Tag = "从剪切板内识别并导入一切以IP格式存在的内容。";
            this.bImport.Text = "导入IP";
            this.bImport.UseVisualStyleBackColor = true;
            this.bImport.Click += new System.EventHandler(this.bImport_Click);
            this.bImport.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Location = new System.Drawing.Point(3, 355);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.Size = new System.Drawing.Size(205, 96);
            this.tbLog.TabIndex = 5;
            // 
            // dgvIpData
            // 
            this.dgvIpData.AllowUserToAddRows = false;
            this.dgvIpData.AllowUserToDeleteRows = false;
            this.dgvIpData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIpData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIpData.Location = new System.Drawing.Point(3, 3);
            this.dgvIpData.Name = "dgvIpData";
            this.dgvIpData.ReadOnly = true;
            this.dgvIpData.RowTemplate.Height = 23;
            this.dgvIpData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvIpData.Size = new System.Drawing.Size(507, 344);
            this.dgvIpData.TabIndex = 0;
            this.dgvIpData.Tag = "右键单击可以强制开启测试。表格数据中：Ping只作为参考值，无实际意义；Tcp80和Tcp443一般只测试443就够了；SslStatus中出现_OK代表IP可用" +
    "；GoAgent中出现_OK代表通过GoAgent测试，IP绝对可用。";
            this.dgvIpData.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dgvIpData, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lTip, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(730, 528);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.AutoSize = true;
            this.tableLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel7.ColumnCount = 6;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tbTimeout, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.tbTestTimeout, 5, 0);
            this.tableLayoutPanel7.Controls.Add(this.label4, 4, 0);
            this.tableLayoutPanel7.Controls.Add(this.tbMaxSockets, 3, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 408);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(507, 21);
            this.tableLayoutPanel7.TabIndex = 17;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel3, 2);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.cbSslStatus, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.cbTcp80, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.cbPing, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.cbTcp443, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 503);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(724, 22);
            this.tableLayoutPanel3.TabIndex = 13;
            // 
            // cbSslStatus
            // 
            this.cbSslStatus.AutoSize = true;
            this.cbSslStatus.Checked = true;
            this.cbSslStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSslStatus.Location = new System.Drawing.Point(546, 3);
            this.cbSslStatus.Name = "cbSslStatus";
            this.cbSslStatus.Size = new System.Drawing.Size(114, 16);
            this.cbSslStatus.TabIndex = 3;
            this.cbSslStatus.Tag = "获取IP所在服务器对应的SSL Status.";
            this.cbSslStatus.Text = "获取 SSL Status";
            this.cbSslStatus.UseVisualStyleBackColor = true;
            this.cbSslStatus.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // cbTcp80
            // 
            this.cbTcp80.AutoSize = true;
            this.cbTcp80.Location = new System.Drawing.Point(184, 3);
            this.cbTcp80.Name = "cbTcp80";
            this.cbTcp80.Size = new System.Drawing.Size(108, 16);
            this.cbTcp80.TabIndex = 0;
            this.cbTcp80.Tag = "用Tcping IP的80端口，与 Tcping 443 端口必须选一个";
            this.cbTcp80.Text = "Tcping 80 端口";
            this.cbTcp80.UseVisualStyleBackColor = true;
            this.cbTcp80.CheckedChanged += new System.EventHandler(this.cbTcp_CheckedChanged);
            this.cbTcp80.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // cbPing
            // 
            this.cbPing.AutoSize = true;
            this.cbPing.Checked = true;
            this.cbPing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPing.Location = new System.Drawing.Point(3, 3);
            this.cbPing.Name = "cbPing";
            this.cbPing.Size = new System.Drawing.Size(48, 16);
            this.cbPing.TabIndex = 1;
            this.cbPing.Tag = "简单地Ping一下IP，Ping通不一定代表IP可用";
            this.cbPing.Text = "Ping";
            this.cbPing.UseVisualStyleBackColor = true;
            this.cbPing.CheckedChanged += new System.EventHandler(this.cbPing_CheckedChanged);
            this.cbPing.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // cbTcp443
            // 
            this.cbTcp443.AutoSize = true;
            this.cbTcp443.Checked = true;
            this.cbTcp443.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTcp443.Location = new System.Drawing.Point(365, 3);
            this.cbTcp443.Name = "cbTcp443";
            this.cbTcp443.Size = new System.Drawing.Size(114, 16);
            this.cbTcp443.TabIndex = 2;
            this.cbTcp443.Tag = "用Tcping IP的443端口，与 Tcping 80 端口必须选一个";
            this.cbTcp443.Text = "Tcping 443 端口";
            this.cbTcp443.UseVisualStyleBackColor = true;
            this.cbTcp443.CheckedChanged += new System.EventHandler(this.cbTcp_CheckedChanged);
            this.cbTcp443.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // bTest
            // 
            this.bTest.AutoSize = true;
            this.bTest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bTest.Dock = System.Windows.Forms.DockStyle.Top;
            this.bTest.Location = new System.Drawing.Point(3, 50);
            this.bTest.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.bTest.Name = "bTest";
            this.bTest.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.bTest.Size = new System.Drawing.Size(205, 32);
            this.bTest.TabIndex = 8;
            this.bTest.Tag = "开始测试各个IP的ping值，tcping 80 和 443 端口 以及获取 SSL Status 信息。";
            this.bTest.Text = "开始测试";
            this.bTest.UseVisualStyleBackColor = true;
            this.bTest.Click += new System.EventHandler(this.bTest_Click_1);
            this.bTest.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 534);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "GoGo Tester 2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIpData)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTestUrl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbIpRange;
        private System.Windows.Forms.Button bAddIpRange;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.CheckBox cbAutoTest;
        private System.Windows.Forms.CheckBox cbAutoSave;
        private System.Windows.Forms.CheckBox cbShutdown;
        private System.Windows.Forms.Label lTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTimeout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTestTimeout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMaxSockets;
        private System.Windows.Forms.DataGridView dgvIpData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Button bExportSelect;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Button bApply;
        private System.Windows.Forms.Button bExportValid;
        private System.Windows.Forms.Button bGaTest;
        private System.Windows.Forms.Button bImport;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox cbSslStatus;
        private System.Windows.Forms.CheckBox cbTcp80;
        private System.Windows.Forms.CheckBox cbPing;
        private System.Windows.Forms.CheckBox cbTcp443;
        private System.Windows.Forms.Button bTest;

    }
}

