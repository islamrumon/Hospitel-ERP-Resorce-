   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  .    (ASITHmsWpf.Accounting.frmReportAccounts1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  
  � Tag$6  0Accounting Information Reports View/Print Screen��   IOUserControl#��[   � 	Resources  �� 
TargetType �� Property �� Value��%/   'R�       �� " R�.���  � ��$
 Bold"�.���  � RowDefinitions �� $
��110* ��� $
��500* � � ColumnDefinitions �� $	��77* ��� $
��323* �.+�R� -    R�Name$ 
tvRptTitle��$��0��
������ $��1��$	��450��$	��Top=�.@�Q� $��A. TRANSACTION REPORTS��$	  000��
����  �.@�Q� $��01. VOUCHER LIST��$  A01TVL��Q� $��02. TRANSACTION LIST��$  A02TL��Q� $��03. CASH BOOK DETAILS��$  A03CBD��Q� $#��B. GENERAL ACCOUNTING REPORTS��$	  000��
����  �.@�Q� $��01. RECEIPTS & PAYMENTS��$  B01RPCB��Q� $��02. CASH & BANK POSITION��$  B02CB��Q� $��03. CASH FLOW STATEMENT��$  B03CF��Q� $��04. CONTROL LEDGER��$  B04CL��Q� $��05. SUBSIDIARY LEDGER��$  B05SL��Q� $��06. CONTROL SCHEDULE��$  B06CS��Q� $��07. CONTROL SCHEDULE-02��$  B07CS2��Q� $��08. SUBSIDIARY SCHEDULE��$  B08SS��Q� $!��C. FINAL ACCOUNTING REPORTS��$	  000��
����  �.@�Q� $��01. TRIAL BALANCE��$  C01TB��Q� $��02. INCOME STATEMENT��$  C02IS��Q� $��03. BALANCE SHEET��$  C03BS����  �� Orientation$ Vertical=�$	��Top=�$��0,10,0,0q�$��0��$��0��$��2��.+��� $ 
Horizontal=�.+��� $ Vertical=�.+��� -   	 ��Name$	 stkOptBranch��$ 
Horizontal=�.+��� $��Branch :��$��90��$��Right=��� -   
 ��Name$
 
cmbSBrnCod�� �� SelectedIndex$ 0��$	��140���� $ 
Horizontal=�.+��� $��Dated From :��$��90��$��Right=�&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtpFrom��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��140���� $ 
Horizontal=�.+��� -    ��Name$ chkAsonDate��$��As On /��$��Center=��� $
��To :��$��30��$��Right=�  -   $ 
xctk_dtpTo��#��[ $
��Bold"�$��22��$	��140���� $ Vertical=�.+��� $ 
Horizontal=�.+��� -   $	 stkOptLocation��$ 
Horizontal=�.+��� $��
Location :��$	��100��$��Right=��� -   $
 
cmbSectCod��$ 0��$	��230���� -	   $	 stkOptVouType��$ 
Horizontal=�.+��� -
    ��Name$ lblVouTypeTitle��$��Vou. _Type :��$��Right=�$��70�� �� Target ��  �� ElementName$ 
cmbVouType���� -   $
 
cmbVouType��
�������$	��190�� �� 
IsReadOnly . $ 0���� -   $	 stkOptActCode��$ 
Horizontal=�.+��� -   $ lblActCodeTitle��$��_Account Head :��$��Right=�$	��100�� �� $ cmbActGroup���� -   $
 cmbActGroup��$ 0��$��75��$��22���� -    ��Name$ txtActCodeSearch��$��50��$��22���� -    ��Name$ btnShowActCode��$	��...��$��15��$��22��$��0q�1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -     Name$ AtxtActCode��   
SearchType$ C��$
��Left=�$��22��$	��350��{�.  �� -   $	 stkOptSirCode��$ 
Horizontal=�.+��� -   $ lblSirCodeTitle��$��_Sub. A/c Head :��$��Right=�$	��100�� �� $ cmbSirGroup���� -   $
 cmbSirGroup��$ 0��$��75��$��22���� -   $ txtSirCodeSearch��$��50��$��22���� -   $ btnShowSirCode��$	��...��$��15��$��22��$��0q�  -   $ AtxtSirCode��$ C��$
��Left=�$��22��$	��350��{�.  �� -   $	 stkOptSchedule��$ 
Horizontal=�.+��� -   $ lblPeriodTitle��$��Period :��$��Right=�$	��100���� -   $
 	cmbPeriod��$ 0��$	��230��$��22��.@��� $(��"Opening, Current & Closing Balance��$  01OCCLB���� $!��Current Period Summary Only��$  02CPSO���� $#��Month Wise Summary (6 Months)��$  03MWS06���� $$��Month Wise Summary (12 Months)��$  04MWS12���� -   $ lblDrCrTitle��$��	Columns :��$��Right=�$��70���� -   $
 cmbDrCr��$ 0��$	��190��$��22��.@��� $ ��Both Debit & Credit Amount��$
  DRCR���� $��Debit Amount Only��$
  DRO1���� $��Credit Amount Only��$
  CRO1���� $��Debit Balance Only��$
  DRB1���� $��Credit Balance Only��$
  CRB1���� $��Net Balance (Dr. - Cr.)��$
  NET1���� $ Vertical=�.+��� $ 
Horizontal=�.+��� $��Output Options :��$	��110��$��Right=��� -   $
 cmbOutputOption��$	��170��$
��Left=�$
��Bold"�$ 0��.@��� $��Print preview��$  PP���� $��Save as PDF file��$	  PDF���� $��Print directly to printer��$  DP���� $��Show in screen��$  SS���� $!��Export to Excel with format��$  EXCELF���� $!��Export to Excel - Data only��$  EXCEL���� $ ��Export to Word with format��$
  WORD���� $��10��$y�Hidden=��� -   $ btnGenerate��  StdBtnStyle01#��[ $��Center=�$��24��$��	_Generate��$
��Bold"�$��80���� -   $	 stkOptTB��$ 
Horizontal=�$��24��$��5,2,0,0q�.+��� -    $ 	chkLevel1�� d� 	IsChecked$
 TrueY�$��Center=�$��60��.��	Level-1�� -!   $ 	chkLevel2��$
 TrueY�$��Center=�$��60��.��	Level-2�� -"   $ 	chkLevel3��$
 TrueY�$��Center=�$��60��.��	Level-3�� -#   $ 	chkLevel4��$
 TrueY�$��Center=�$��60��.��	Level-4�� -$   $	 
stkOptMore��$ 
Horizontal=�$y�Visible=�.+��� $��More :��$��Right=�$��45��$��0,-2,0,0q��� -%   $
 cmbSubLevel��$ 0��$��80��$��22��.@��� $
��None��$
  NONE���� $��Branch��$  BRANCH���� $��Location��$  LOCATION���� $��Level-1��$  	SUBLEVEL1���� $��Level-2��$  	SUBLEVEL2���� $��Level-3��$  	SUBLEVEL3���� $��Level-4��$  	SUBLEVEL4���� $��Level-5��$  	SUBLEVEL5���� -&   $	 stkOptGroup��$ 
Horizontal=�$��24��$
��Left=�$��5,2,0,0q�.+��� -'   $ chkGroup��$
 TrueY�$��Center=�$��90��.��
Show Group�� -(   $	 stkRptOptions��$ 
Horizontal=�.+��� -)   $ lblRptOptions��$��	Options :��$��Right=�$��60���� -*   $
 cmbRptOptions��$ 0��$	��220��$��22��.@��� $
��None��$
  NONE���� $��1��$��1��$��2��.+��� $��0q�
��� ����� $��45��$��Center=�$	��950��.+��� -+   $ lbltle1�����	#FF862222$��Center=�$
��Bold"�$��Verdana)��� -,   $ lbltle2�����	#FF862222$��Center=�$
��Bold"�$��Verdana)�� .+��� .+�&   System.Windows.Controls.DataGrid  --     Name$ dgOverall01��   AlternatingRowBackground �	#FFE4F2E1   
IsReadOnly .    CanUserSortColumns .  $	��950��$��Center=�$	��450��$��Visible=� �� 
GroupStyle (  "System.Windows.Controls.GroupStyle  -.   	grp11��   ContainerStyle �� " ��.���  � �� ��  �� 
TargetType" ��.R�9�  9�Name$ expsty1�� 9� 
IsExpanded .  
�������$��1q����� .+��� $
��Bold"�
����  �$��12(�  �� TextAlignment$  Center=����� ! �� Path$
! Name����� 