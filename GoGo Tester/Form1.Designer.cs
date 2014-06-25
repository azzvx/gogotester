namespace GoGo_Tester
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.tbIpRange = new System.Windows.Forms.TextBox();
            this.bAddIpRange = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.nMaxThreads = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nPingTimeout = new System.Windows.Forms.NumericUpDown();
            this.nTestTimeout = new System.Windows.Forms.NumericUpDown();
            this.cmsIpData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mImportIpsInClipbord = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mExportSelectedIps = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mExportAllIps = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mRemoveSelectedIps = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mRemoveAllIps = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mRemoveIpsInClipbord = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mApply = new System.Windows.Forms.ToolStripMenuItem();
            this.mApplySelectedIpsToUserConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mApplyValidIpsToUserConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mStartRndTest = new System.Windows.Forms.ToolStripMenuItem();
            this.mStartStdTest = new System.Windows.Forms.ToolStripMenuItem();
            this.mStopTest = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvIpData = new System.Windows.Forms.DataGridView();
            this.lTip = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lProgress = new System.Windows.Forms.Label();
            this.mRemoveInvalidIps = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPingTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nTestTimeout)).BeginInit();
            this.cmsIpData.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIpData)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel5, 2);
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tbIpRange, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.bAddIpRange, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 344);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(476, 61);
            this.tableLayoutPanel5.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 28);
            this.label5.TabIndex = 0;
            this.label5.Text = "导入IP段：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbIpRange
            // 
            this.tbIpRange.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbIpRange.Location = new System.Drawing.Point(74, 3);
            this.tbIpRange.Name = "tbIpRange";
            this.tbIpRange.Size = new System.Drawing.Size(334, 21);
            this.tbIpRange.TabIndex = 1;
            this.tbIpRange.Tag = "允许添加IP段，格式 0-255 或 0/255 或 0\\255。 173.0-255.0\\255.0/255 代表 173 段所有 IP ，其它同理。不同IP段" +
    "请用除\'. - \\ / \'以外的符号分隔。";
            this.tbIpRange.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // bAddIpRange
            // 
            this.bAddIpRange.AutoSize = true;
            this.bAddIpRange.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bAddIpRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bAddIpRange.Location = new System.Drawing.Point(414, 3);
            this.bAddIpRange.Name = "bAddIpRange";
            this.bAddIpRange.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.bAddIpRange.Size = new System.Drawing.Size(59, 22);
            this.bAddIpRange.TabIndex = 2;
            this.bAddIpRange.Text = "导入";
            this.bAddIpRange.UseVisualStyleBackColor = true;
            this.bAddIpRange.Click += new System.EventHandler(this.bAddIpRange_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel5.SetColumnSpan(this.tableLayoutPanel2, 3);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.nMaxThreads, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.nPingTimeout, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.nTestTimeout, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 31);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(470, 27);
            this.tableLayoutPanel2.TabIndex = 27;
            // 
            // nMaxThreads
            // 
            this.nMaxThreads.Dock = System.Windows.Forms.DockStyle.Top;
            this.nMaxThreads.Location = new System.Drawing.Point(74, 3);
            this.nMaxThreads.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nMaxThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nMaxThreads.Name = "nMaxThreads";
            this.nMaxThreads.Size = new System.Drawing.Size(79, 21);
            this.nMaxThreads.TabIndex = 27;
            this.nMaxThreads.Tag = "";
            this.nMaxThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nMaxThreads.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nMaxThreads.ValueChanged += new System.EventHandler(this.nMaxTest_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 27);
            this.label3.TabIndex = 8;
            this.label3.Tag = "随机测试和标准测试的最大线程数";
            this.label3.Text = "最大线程：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(159, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 27);
            this.label1.TabIndex = 29;
            this.label1.Tag = "Ping的延时。";
            this.label1.Text = "最大延时：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(315, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 27);
            this.label4.TabIndex = 10;
            this.label4.Tag = "（如无必要请勿修改）测试的超时时间";
            this.label4.Text = "测试超时：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // nPingTimeout
            // 
            this.nPingTimeout.Dock = System.Windows.Forms.DockStyle.Top;
            this.nPingTimeout.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nPingTimeout.Location = new System.Drawing.Point(230, 3);
            this.nPingTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nPingTimeout.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nPingTimeout.Name = "nPingTimeout";
            this.nPingTimeout.Size = new System.Drawing.Size(79, 21);
            this.nPingTimeout.TabIndex = 30;
            this.nPingTimeout.Tag = "";
            this.nPingTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nPingTimeout.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nPingTimeout.ValueChanged += new System.EventHandler(this.nPingTimeout_ValueChanged);
            // 
            // nTestTimeout
            // 
            this.nTestTimeout.Dock = System.Windows.Forms.DockStyle.Top;
            this.nTestTimeout.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nTestTimeout.Location = new System.Drawing.Point(386, 3);
            this.nTestTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nTestTimeout.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nTestTimeout.Name = "nTestTimeout";
            this.nTestTimeout.Size = new System.Drawing.Size(81, 21);
            this.nTestTimeout.TabIndex = 28;
            this.nTestTimeout.Tag = "";
            this.nTestTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nTestTimeout.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.nTestTimeout.ValueChanged += new System.EventHandler(this.nTestTimeout_ValueChanged);
            // 
            // cmsIpData
            // 
            this.cmsIpData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mImport,
            this.toolStripSeparator1,
            this.mExport,
            this.toolStripSeparator2,
            this.mRemove,
            this.toolStripSeparator3,
            this.mApply});
            this.cmsIpData.Name = "cmsIpData";
            this.cmsIpData.Size = new System.Drawing.Size(117, 110);
            // 
            // mImport
            // 
            this.mImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mImportIpsInClipbord});
            this.mImport.Name = "mImport";
            this.mImport.Size = new System.Drawing.Size(152, 22);
            this.mImport.Text = "导入(&I)";
            // 
            // mImportIpsInClipbord
            // 
            this.mImportIpsInClipbord.Name = "mImportIpsInClipbord";
            this.mImportIpsInClipbord.Size = new System.Drawing.Size(187, 22);
            this.mImportIpsInClipbord.Text = "剪切板中存在的IP(&C)";
            this.mImportIpsInClipbord.Click += new System.EventHandler(this.mImportIpsInClipbord_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // mExport
            // 
            this.mExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mExportSelectedIps,
            this.toolStripSeparator6,
            this.mExportAllIps});
            this.mExport.Name = "mExport";
            this.mExport.Size = new System.Drawing.Size(152, 22);
            this.mExport.Text = "导出(&E)";
            // 
            // mExportSelectedIps
            // 
            this.mExportSelectedIps.Name = "mExportSelectedIps";
            this.mExportSelectedIps.Size = new System.Drawing.Size(186, 22);
            this.mExportSelectedIps.Text = "选中的IP到剪切板(&S)";
            this.mExportSelectedIps.Click += new System.EventHandler(this.mExportSelectedIps_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(183, 6);
            // 
            // mExportAllIps
            // 
            this.mExportAllIps.Name = "mExportAllIps";
            this.mExportAllIps.Size = new System.Drawing.Size(186, 22);
            this.mExportAllIps.Text = "全部IP到剪切板(&A)";
            this.mExportAllIps.Click += new System.EventHandler(this.mExportAllIps_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // mRemove
            // 
            this.mRemove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mRemoveSelectedIps,
            this.toolStripSeparator4,
            this.mRemoveAllIps,
            this.toolStripSeparator8,
            this.mRemoveIpsInClipbord,
            this.toolStripSeparator7,
            this.mRemoveInvalidIps});
            this.mRemove.Name = "mRemove";
            this.mRemove.Size = new System.Drawing.Size(152, 22);
            this.mRemove.Text = "移除(&R)";
            // 
            // mRemoveSelectedIps
            // 
            this.mRemoveSelectedIps.Name = "mRemoveSelectedIps";
            this.mRemoveSelectedIps.Size = new System.Drawing.Size(187, 22);
            this.mRemoveSelectedIps.Text = "选中的IP(&S)";
            this.mRemoveSelectedIps.Click += new System.EventHandler(this.mRemoveSelectedIps_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(184, 6);
            // 
            // mRemoveAllIps
            // 
            this.mRemoveAllIps.Name = "mRemoveAllIps";
            this.mRemoveAllIps.Size = new System.Drawing.Size(187, 22);
            this.mRemoveAllIps.Text = "全部IP(&A)";
            this.mRemoveAllIps.Click += new System.EventHandler(this.mRemoveAllIps_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(184, 6);
            // 
            // mRemoveIpsInClipbord
            // 
            this.mRemoveIpsInClipbord.Name = "mRemoveIpsInClipbord";
            this.mRemoveIpsInClipbord.Size = new System.Drawing.Size(187, 22);
            this.mRemoveIpsInClipbord.Text = "剪切板中存在的IP(&C)";
            this.mRemoveIpsInClipbord.Click += new System.EventHandler(this.mRemoveIpsInClipbord_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // mApply
            // 
            this.mApply.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mApplySelectedIpsToUserConfig,
            this.toolStripSeparator5,
            this.mApplyValidIpsToUserConfig});
            this.mApply.Name = "mApply";
            this.mApply.Size = new System.Drawing.Size(152, 22);
            this.mApply.Text = "应用(&A)";
            // 
            // mApplySelectedIpsToUserConfig
            // 
            this.mApplySelectedIpsToUserConfig.Name = "mApplySelectedIpsToUserConfig";
            this.mApplySelectedIpsToUserConfig.Size = new System.Drawing.Size(223, 22);
            this.mApplySelectedIpsToUserConfig.Text = "选中的IP到用户配置文件(&S)";
            this.mApplySelectedIpsToUserConfig.Click += new System.EventHandler(this.mApplySelectedIpsToUserConfig_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(220, 6);
            // 
            // mApplyValidIpsToUserConfig
            // 
            this.mApplyValidIpsToUserConfig.Name = "mApplyValidIpsToUserConfig";
            this.mApplyValidIpsToUserConfig.Size = new System.Drawing.Size(223, 22);
            this.mApplyValidIpsToUserConfig.Text = "可用的IP到用户配置文件(&V)";
            this.mApplyValidIpsToUserConfig.Click += new System.EventHandler(this.mApplyValidIpsToUserConfig_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.Controls.Add(this.msMain, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvIpData, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lTip, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.pbProgress, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lProgress, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(482, 438);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // msMain
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.msMain, 2);
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mStartRndTest,
            this.mStartStdTest,
            this.mStopTest});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(482, 25);
            this.msMain.TabIndex = 3;
            this.msMain.Text = "menuStrip1";
            // 
            // mStartRndTest
            // 
            this.mStartRndTest.Name = "mStartRndTest";
            this.mStartRndTest.Size = new System.Drawing.Size(108, 21);
            this.mStartRndTest.Tag = "随机从自带的100万IP池中获取指定数量的可用IP。如果指定的数目过于庞大，可能会花费很长时间进行测试。";
            this.mStartRndTest.Text = "开始随机测试(&R)";
            this.mStartRndTest.Click += new System.EventHandler(this.mStartRndTest_Click);
            this.mStartRndTest.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // mStartStdTest
            // 
            this.mStartStdTest.Name = "mStartStdTest";
            this.mStartStdTest.Size = new System.Drawing.Size(107, 21);
            this.mStartStdTest.Tag = "测试IP是否可用。";
            this.mStartStdTest.Text = "开始标准测试(&S)";
            this.mStartStdTest.Click += new System.EventHandler(this.mStartStdTest_Click);
            this.mStartStdTest.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // mStopTest
            // 
            this.mStopTest.Name = "mStopTest";
            this.mStopTest.Size = new System.Drawing.Size(83, 21);
            this.mStopTest.Tag = "停止所有测试。";
            this.mStopTest.Text = "停止测试(&T)";
            this.mStopTest.Click += new System.EventHandler(this.mStopTest_Click);
            this.mStopTest.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // dgvIpData
            // 
            this.dgvIpData.AllowUserToAddRows = false;
            this.dgvIpData.AllowUserToDeleteRows = false;
            this.dgvIpData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dgvIpData, 2);
            this.dgvIpData.ContextMenuStrip = this.cmsIpData;
            this.dgvIpData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIpData.Location = new System.Drawing.Point(3, 44);
            this.dgvIpData.Name = "dgvIpData";
            this.dgvIpData.ReadOnly = true;
            this.dgvIpData.RowTemplate.Height = 23;
            this.dgvIpData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIpData.Size = new System.Drawing.Size(476, 294);
            this.dgvIpData.TabIndex = 25;
            this.dgvIpData.Tag = "表格数据中：标准测试在50ms以内为优秀，代理测试在1000ms以内为优秀。";
            this.dgvIpData.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // lTip
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lTip, 2);
            this.lTip.Dock = System.Windows.Forms.DockStyle.Top;
            this.lTip.ForeColor = System.Drawing.Color.DarkRed;
            this.lTip.Location = new System.Drawing.Point(3, 408);
            this.lTip.Name = "lTip";
            this.lTip.Padding = new System.Windows.Forms.Padding(3);
            this.lTip.Size = new System.Drawing.Size(476, 30);
            this.lTip.TabIndex = 12;
            this.lTip.Text = "Tip\r\nTip";
            // 
            // pbProgress
            // 
            this.pbProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbProgress.Location = new System.Drawing.Point(3, 28);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(336, 10);
            this.pbProgress.Step = 1;
            this.pbProgress.TabIndex = 23;
            // 
            // lProgress
            // 
            this.lProgress.AutoSize = true;
            this.lProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.lProgress.Location = new System.Drawing.Point(345, 25);
            this.lProgress.Name = "lProgress";
            this.lProgress.Size = new System.Drawing.Size(134, 12);
            this.lProgress.TabIndex = 24;
            this.lProgress.Tag = "随机测试 线程数 / 完成数；标准测试：线程数 / 剩余数";
            this.lProgress.Text = "0 / 0";
            this.lProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lProgress.MouseEnter += new System.EventHandler(this.Tip_MouseEnter);
            // 
            // mRemoveInvalidIps
            // 
            this.mRemoveInvalidIps.Name = "mRemoveInvalidIps";
            this.mRemoveInvalidIps.Size = new System.Drawing.Size(187, 22);
            this.mRemoveInvalidIps.Text = "无效的IP(&I)";
            this.mRemoveInvalidIps.Click += new System.EventHandler(this.mRemoveInvalidIps_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(184, 6);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 444);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "GoGo Tester 2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPingTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nTestTimeout)).EndInit();
            this.cmsIpData.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIpData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbIpRange;
        private System.Windows.Forms.Button bAddIpRange;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip cmsIpData;
        private System.Windows.Forms.ToolStripMenuItem mImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mExport;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem mStartStdTest;
        private System.Windows.Forms.ToolStripMenuItem mImportIpsInClipbord;
        private System.Windows.Forms.ToolStripMenuItem mExportSelectedIps;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mRemove;
        private System.Windows.Forms.ToolStripMenuItem mRemoveSelectedIps;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mExportAllIps;
        private System.Windows.Forms.ToolStripMenuItem mRemoveAllIps;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem mRemoveIpsInClipbord;
        private System.Windows.Forms.Label lProgress;
        private System.Windows.Forms.DataGridView dgvIpData;
        private System.Windows.Forms.NumericUpDown nMaxThreads;
        private System.Windows.Forms.NumericUpDown nTestTimeout;
        private System.Windows.Forms.Label lTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStripMenuItem mStopTest;
        private System.Windows.Forms.ToolStripMenuItem mStartRndTest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mApply;
        private System.Windows.Forms.ToolStripMenuItem mApplyValidIpsToUserConfig;
        private System.Windows.Forms.NumericUpDown nPingTimeout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem mApplySelectedIpsToUserConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem mRemoveInvalidIps;

    }
}

