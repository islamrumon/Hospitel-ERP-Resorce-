   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  ,    &ASITHmsWpf.Accounting.frmEntryVoucher1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4UC%clr-namespace:ASITHmsWpf.UserControls     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -   
  � Tag$*  $Accounts Voucher - Entry/Edit Screen��   IOUserControl#��[  .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� -    �Name$ 
gridMaster��$��0��$	��Top=�$��	10,0,10,0q�$��2�� �� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��245* ��� $
��174* ��� $
��181* �.+��� $��0��$��0��$
��Left=� �� Orientation$ 
Horizontal=�.+��� $��New/Selected Tranasction:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� -    ��Name$ txtblEditMode��$y�Hidden=�$��[ Voucher In Edit Mode ]��$��14(� �� TextAlignment$ Center=�
��� ���$
��Bold"�
���  ��$	��180��$��20,0,0,0q��� -    ��Name$ stkIntro��$ Vertical=�$��0��$��2��$��1��$��3��$	��Top=�$
��Left=�$��88��$��	0,3,3.6,0q�.+��� $ 
Horizontal=�.+��� -    ��Name$ lblVouTypeTitle��$��Voucher _Type:��$��Right=�$	��120��$��14(�$
��Bold"�	 �� Target	 �� 
 �� ElementName$
 
cmbVouType���� -    ��Name$ 
cmbVouType��
�������$	��280�� �� 
IsReadOnly .  �� SelectedIndex$ 0��$��14(�$
��Bold"��� -   $ lblVouBrnTitle��$��_Branch:��$��Right=�$��50��	 �� $
 	cmbVouBrn���� -   $ 	cmbVouBrn��
�������$	��180�� . $ 0���� -	   $ lblVouRefTitle��$��_Referance:��$��Right=�$��70��	 �� $
 	txtVouRef��(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -
     Name$ 	txtVouRef��$��0�� 
 wtxtBx#��[    	Watermark$ Voucher Ref. (if any)��$��24�� �� TextWrapping$
 Wrap=�$	��100��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .    AutoSelectBehavior$ OnFocus=��� $ 
Horizontal=�$��0,2,0,0q�.+��� -   $ lblVouNarTitle��$��Narration (if any):��$	��Top=�$��Right=�$��25��$	��120��	 �� $
 	txtVouNar���� -    ��Name$ 	txtVouNar��$�� ��$
��Left=� J� 	AllowDrop . $
��Auto=� �� AcceptsReturn . $	��680��$��55��$
 Wrap=��� $ Vertical=�$��2��$��1��$��Right=�$��80��.+��� $ 
Horizontal=�.+��� -    ��Name$ chkDateBlocked��$��Voucher _Date : ��|�.  $��Right=�$��Center=�$	��115��&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtpVouDat��  StdDateTimePickerStyle01#��[ $
��Bold"�$��24��$	��130��{�.  �� $��20��$y�Hidden=��� -    ��Name$ btnOk��  StdBtnStyle01#��[ $	��_Ok��$��75���� $ 
Horizontal=�.+��� -   $ lblVouNoTitle��$��Voucher No:��$��Right=�$	��115���� -   $ lblVouNo��$��XVXMM-CCCC-XXXXX��$  XVXYYYYMMCCCCXXXXX��$	��140��$
��Bold"�$
��Left=��� $ 
Horizontal=�$��Right=�$��0, -2, 0, 0q�.+��� -   $ chkAutoTransList��$��_Show transactions list��|�.  $��Center=��� $��35��$��22��$y�Hidden=��� -   $ 	btnPrint2��$��_Print��$��75��$��12(�$��Verdana)�#��[ �� $��20��$y�Hidden=��� -   $ 	btnUpdate��#��[ $��_Update��$��75���� $��1��$	��Top=�
��� ���$��5��$��2��$��0,2,0,0q��� $��2��.+�� -   $ gridDetails��$��2��$	��Top=�$y�Visible=�$��	10,5,10,0q�$��2��$	��130��.+��� .+��� -   $ stkpControl��$ 
Horizontal=�$��26��.+��� $ 
Horizontal=�.+��� -   $ lblCactCodeTitle��$��Bank/Ca_sh:��$��Right=�$	��120��	 �� $
 AtxtCactCode��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -     Name$ AtxtCactCode��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox   MouseDoubleClick$# AtxtCactCode_MouseDoubleClick��   GotFocus$ AtxtCactCode_GotFocus��   	LostFocus$ AtxtCactCode_LostFocus��   
SearchType$ C��$
��Left=�$	��680�� � ToolTip$8 2Right Click or Double Click to view Cash/Bank list�� � ContextMenu �� -     ��Name$  conMenuCactCode��$	��200��$	��400���� $ 
Horizontal=�.+��� -   $ 
stkpCheqNo��$ 
Horizontal=�$	��215��.+��� -   $ lblCheqNoTitle��$��
Cheque No:��$��Right=�$��85��	 �� $
 	cmbCheqNo���� -   $ 	cmbCheqNo��$	��130��
������� . $ 0���� -   $ lblAdviceTitle��$��Advice:��$��Right=�$��50��	 �� $
 	txtAdvice��  -   $ 	txtAdvice��! �� TextAlignment$
! Left=� . $ OnFocus=�$��24��#��[ $  ��$�� ��$
��Left=�$	��115���� $��5���� $ Vertical=�.+��� $ 
Horizontal=�$��0,5,0,0q�.+��� -   $ 
txtActCode��$��10��" �� 
IsReadOnly" . $��0q�
������ �� -    $ lblActCodeTitle��$��Account _Head:��$��Right=�$	��110��	 �� $
 AtxtActCode��  -!   $ AtxtActCode��$ AtxtActCode_LostFocus��$ C��$
��Left=�$	��680��$��26���� -"   $ 	lblLevel2��$��2��$��20��$
��Bold"�
���  ��$y�Hidden=��� -#   $ stkpLocation��$ 
Horizontal=�.+��� -$   $ lblSectCodTitle��$��
_Location:��$��Right=�$��65��	 �� $
 AtxtSectCod��  -%   $ AtxtSectCod��$" AtxtSectCod_MouseDoubleClick��$ AtxtSectCod_LostFocus��$ C��$
��Left=�$	��295��$��26��$3 -Right Click or Double Click to view loactions�� �� -&   $  conMenuSectCod��$	��400��$	��295���� -'   $ stkpSubHead��$��26��$ 
Horizontal=�$��0,5,0,0q�.+��� $ 
Horizontal=�$��	100,0,0,0q�.+��� -(   $ lblSirCodeTitle��$��	Sub.Head:��$��Right=�$	��100��	 �� $
 AtxtSirCode��  -)   $ AtxtSirCode��$ AtxtSirCode_LostFocus��$ C��$
��Left=�$	��600��$��26���� -*   $ stkpQty��$ 
Horizontal=�.+��� -+   $ lblQtyTitle��$��	Quantity:��$��Right=�$��85��$��26��	 �� $
 txtQty��  -,   $ txtQty��$! Right=�$��Center=� . $ OnFocus=�#��[ $  ��$�� ��$
��Left=�$��24��$	��100���� --   $ lblUnit��$�� ��$
��Left=�$��30��	 �� $
 txtQty���� -.   $ lblRateTitle��$��Rate:��$��Right=�$��50��$��26��	 �� $
 txtRate��  -/   $ txtRate��$! Right=�$��Center=� . $ OnFocus=�$��5,0,0,0q�#��[ $  ��$�� ��$
��Left=�$��24��$	��115���� -0   $ stkpSubHead2��$��26��$ 
Horizontal=�$��	100,2,0,0q�.+��� -1   $ lblSirCode2Title��$��Sub.Head-2:��$��Right=�$	��100��	 �� $
 AtxtSirCode2��  -2   $ AtxtSirCode2��$ AtxtSirCode2_LostFocus��$ C��$��26��$
��Left=�$	��600���� $ 
Horizontal=�$��40,2,0,0q�.+��� -3   $ stkpChk1��$ 
Horizontal=�
�������.+��� -4   $ 
chkSubHead��$��Sub Head��$��Center=�$��80��|�.  �� -5   $ chkQty��$��Quantity��$��Center=�$��80��|�.  �� -6   $ chkSubHead2��$��
Sub Head-2��$��Center=�$	��100��|�.  �� -7   $ lblRmrkTitle��$�� ��$��Right=�$��50��	 �� $
 txtRmrk���� -8   $ chkRmrk��$��Rrmarks (if any):��$��Center=�$	��110��|�.    -9   $ txtRmrk��$
! Left=�$��Center=� . $ OnFocus=�$��26��$��5,0,0,0q�#��[ $  ��$�� ��$
��Left=�$	��340��|��� $
 chkRmrk��# �� Path$# 	IsChecked��� -:   $ lblAmountTitle��$��Am_ount:��$��Right=�$��85��	 �� $
 	txtAmount��  -;   $ 	txtAmount��$! Right=�$��Center=� . $ OnFocus=�$��5,0,0,0q�#��[ $  ��$�� ��$
��Left=�$��26��$	��130���� -<   $ cmbDrCr��$��70��
������� . $ 0��$��2,0,0,0q�.@��� $��Debit���� $��Credit���� -=   $ btnAdd��#��[ $��
Add Record��$��80��$��13,0,0,0q�� ->   $ gridDetails1��$��3��$	��Top=�$y�Hidden=�$��10q�$��2��.+��� .+�&   System.Windows.Controls.DataGrid  -?   $  Name$$ dgTrans��%   CanUserResizeRows% .  
������ &   
IsReadOnly& .  $��0q�$��0,0,0,0q�$��0��$	��Top=�$	��250��$
��Auto=�'   AutoGenerateColumns' .  (   CanUserAddRows( .  )   CanUserDeleteRows) .  $��Hidden=�*   SelectionUnit$
* Cell=�+   SelectionMode$+ Single=�,   Columns, 4  .System.Windows.Controls.DataGridTemplateColumn  -@   dgTransColSl��,  &System.Windows.Controls.DataGridColumn-   Width5	  /System.Windows.Controls.DataGridLengthConverter$- 40	 .   Header$	. Sl#��/   CellTemplate/ �� .R�� $��Right=�$	��Top=�$��40��.+��� $��Right=�$	��Top=����� *trnsl+0 �� StringFormat$0 
{0: ##0) }��0
  *System.Windows.Controls.DataGridTextColumn
  -A   dgTransColAcHead��$	- 450	 $. Account Head��1   CanUserSort1 .  2   
IsReadOnly2 . 1  +System.Windows.Controls.DataGridBoundColumn3   Binding3 �� $# trnDesc�
  -B   dgTransColLoc��$	- 120	 $. Location��4 +� IsFocusScope4 .  1 .  2 . 5   CanUserReorder5 .  6   CanUserResize6 .  3 �� $# sectcodDesc�  -C   dgTransColQty��$- 65	 $. Quantity��2 .  / �� .R�  -D   $! Right=� . $��0q�$��60��$ OnFocus=�$��5,0,0,0q�#��[ $ Quantity������ $# trnqty�7 �� Mode$7 TwoWay=�8 �� UpdateSourceTrigger$8 PropertyChanged=�$0 {0: #,##0.00}��  -E   dgTransColUnit��$- 45	 $
. Unit��/ �� .R��� $	��Top=�$��40��$��22������ $# trnUnit�  -F   dgTransColRate��$- 80	 $
. Rate��/ �� .R��� $��Right=�$	��Top=����� *	trnrate+$0 {0: #,##0.00}��  -G   dgTransColDr��$	- 110	 $. Debit Amount��/ �� .R�  -H   $! Right=� . $��0q�$	��100��$ OnFocus=�$��5,0,0,0q�#��[ $ 
Dr. Amount������ $# dramt�$7 TwoWay=�$8 PropertyChanged=�$ 0 {0: #,##0.00;(#,##0.00); }��  -I   dgTransColCr��$	- 110	 $. Credit Amount��/ �� .R�  -J   $! Right=� . $��0q�$	��100��$ OnFocus=�$��5,0,0,0q�#��[ $ 
Cr. Amount������ $# cramt�$7 TwoWay=�$8 PropertyChanged=�$ 0 {0: #,##0.00;(#,##0.00); }��  -K   dgTransColRmrk��$	- 140	 $. Remarks��/ �� .R�  $
! Left=� . $��0q�$	��130��$ OnFocus=�$��5,0,0,0q�#��[ $  ������ $# trnrmrk�$7 TwoWay=�$8 PropertyChanged=��� $ 
Horizontal=�.+��� $y�Hidden=����� $
 dgTransColSl��$# ActualWidth��� $y�Hidden=����� $
 dgTransColAcHead��$# ActualWidth��� ���� $
 dgTransColLoc��$# ActualWidth�.+��� -L   $ btnTotal��$��_Total: ��$��Right=�
������ $
��Bold"�$��0q�$��80���� -M   9 ��Name$9 SeprQty��$y�Hidden=����� $
 dgTransColQty��$# ActualWidth��� -N   $9 SeprUnit��$y�Hidden=����� $
 dgTransColUnit��$# ActualWidth��� -O   $9 SeprRate��$y�Hidden=����� $
 dgTransColRate��$# ActualWidth��� -P   $ 
lblSumDram��$��Right=����� $
 dgTransColDr��$# ActualWidth��� -Q   $ 
lblSumCram��$��Right=����� $
 dgTransColCr��$# ActualWidth��� $y�Hidden=����� $
 dgTransColRmrk��$# ActualWidth�� -R   $ gridTransList��$��2��$	��Top=�$y�Visible=�
�������$��	10,0,10,0q�.+��� $ Vertical=�$	��Top=�$	��450��.+��� $ 
Horizontal=�$
��Left=�.+��� $!��Existing Tranasctions List:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� $��10��$y�Hidden=��� -S   $ txtTransTitle��$>��8Transaction Date Range From: 01-Dec-2015 To: 15-Dec-2015��$
��Bold"�$ Center=�$	��800���� -T   : ��Name$: lvTransList��$	��350��$��14(�$
��Auto=�$��Hidden=����� ; �� 
TargetType"; ��.��� < �� Property< � ��= �� Value= �	#FFE4F2E1> �� View> �� .P��� ? �� Width$? 40��$	��Sl#��@ �� CellTemplate@ 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $��Right=�$	��Top=�$��40��.+��� $��Right=�$	��Top=����� *slnum+�� $�.���� $	? 130��$
��Date��A �� DisplayMemberBindingA �� $# voudat1��� $	? 140��$��Voucher No.��$
��Bold"�A �� $# vounum1��� $	? 140��$��Voucher Amount��$
��Bold"�A �� $# vounum1�$ 0 {0: #,##0.00;(#,##0.00); }���� $	? 100��$��Voucher Ref.��A �� $# vouref��� $	? 100��$��	Cheqe No.��A �� $# chqref��� $	? 100��$��Advice Ref.��A �� $# advref��� $? 50��$��Status��A �� $# vstatus��� $��5��]� .+��� $ 
Horizontal=�$
��Left=�$
��Left=�.+��� -U   $ btnRecurring��$��	Recurring��#��[ $��80��$��22��{�.  �� $��10��$y�Hidden=��� -V   $ 	chkFilter��B d� 	IsChecked$B FalseY�$��Filter Records��$��Center=�$��Center=�� C � AllowsTransparencyC . $��Slide=����� $
 	chkFilter��$# 	IsChecked�D � PlacementTargetD �� $
 	chkFilter��.���� $��0,0,5,5q�$��0q�E �� CornerRadius$E 8������ F �� EndPoint$	F 0,1&�G �� 
StartPoint$	G 0,1&�.��� H � Color$H 	#FFECFBFB��I � Offset$	I 0.0Q�� $H 	#FFCED9E4��$	I 0.5Q�� $H 	#FF5F6E6E��$	I 1.0Q����� $F 0.5,1&�$G 0.5,0&�.��� $H 	#FFF7F7F7��$	I 0.0Q�� $H 	#FFCED9E4��$	I 0.5Q�� $H 	#FF22F5F5��$	I 1.0Q�J J� EffectJ 3  -System.Windows.Media.Effects.DropShadowEffect  K   Color$K Black��L   ShadowDepth$L 5Q�M   
BlurRadius$M 20Q�N   Opacity$	N 0.8Q�O   RenderingBias$O Quality=�._��� $	��340��$��90������ $F 0.5,1&�$G 0.5,0&�.��� $H 	#FFE4F2E1��$I 0.491Q�� $H 	#FF69B28B��$I 0.957Q�z��� -W   theTransform1��.+��� $ Vertical=�.+��� $1��+Filter Option For Getting Transaction List:��$��12(�$��Italic$�$	��340��$��Center=�|�. ���� $F 0.5,1&�P � MappingMode$P RelativeToBoundingBox=�$G 0.5,0&�.��� $H White��� $H 	#FFB7B7B7��$
I 0.46Q�� $H 	#FFDDDDDD��$I 0.233Q��� $ 
Horizontal=�.+��� $��From :��$��12(�$��Italic$�$��45��$
��Left=�  -X   $ xctk_dtpFromDate��#��[ $��20��$	��130���� $
��To :��$��Italic$�$��28��$��Center=�  -Y   $ xctk_dtpToDate��#��[ $��20��$	��130���� -Z   $ 	btnFilter��$��	Go Filter��$��20��$��60��$��12(�$��Verdana)�$��0,10,0,0q�#��[ Q �� Left$	Q 155��
R �� Top$	R 125���� $��30��$y�Hidden=��� $ 
Horizontal=�$��Right=�$��Right=�.+��� $��10��$y�Hidden=��� -[   $ 
btnVouEdit��$
��Edit��#��[ $	��100��$��22���� $��10��$y�Hidden=��� -\   $ btnVouCancel��$��Cancel/Remove��#��[ $	��100��$��22��$ Remove entry���� $��10��$y�Hidden=��� -]   $ 	chkPrint2��$B FalseY�$��Print Memo/List��$��Center=�$��Center=�� C . $��Slide=����� $
 	chkPrint2��$# 	IsChecked�D �� $
 	chkPrint2��.���� $��0,0,5,5q�$��0q�$E 8������ $	F 0,1&�$	G 0,1&�.��� $H 	#FFECFBFB��$	I 0.0Q�� $H 	#FFCED9E4��$	I 0.5Q�� $H 	#FF5F6E6E��$	I 1.0Q����� $F 0.5,1&�$G 0.5,0&�.��� $H 	#FFF7F7F7��$	I 0.0Q�� $H 	#FFCED9E4��$	I 0.5Q�� $H 	#FF22F5F5��$	I 1.0Q�J   $K Black��$L 5Q�$M 20Q�$	N 0.8Q�$O Quality=�._��� $	��134��$	��146������ $F 0.5,1&�$G 0.5,0&�.��� $H 	#FFE4F2E1��$I 0.491Q�� $H 	#FF69B28B��$I 0.957Q�z��� -^   theTransform2��.+��� $ Vertical=�$	��134��.+��� $��Print Options��$��12(�$��Italic$�$
��Bold"�
���**��$
��Left=��� $ Vertical=�$��10,0,0,0q�.+�	� -_   S 	�Name$S rb3SelectedMemo��$��Selected Memo��$
B TrueY��� $y�Hidden=�	� -`   $S rb3TableRecoreds��$��Table Records���� �� $ Vertical=�$��10,2,0,0q�.+�	� -a   $S rb3QuickPrint��$��Quick Print���� $y�Hidden=�	� -b   $S rb3PrintPreview��$��Print Preview��$
B TrueY��� -c   $ 	btnPrint3��$��Go Print��$��20��$��60��$��12(�$��Verdana)�#��[ $Q 34��$	R 116���� $��20��$y�Hidden=�