   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  .    (ASITHmsWpf.Inventory.frmEntryPurReqAppr1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  -   
  � Tag$7  1Purchase Requisition Approval - Entry/Edit Screen��   IOUserControl#��[  .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� -    �Name$ 
gridMaster��$��0��$	��Top=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��245* ��� $
��174* ��� $
��181* �.+��� $��0��$��0��$
��Left=� �� Orientation$ 
Horizontal=�.+��� $�� Approval   Section:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� -    ��Name$ stkIntro��$ Vertical=�$��0��$��2��$��1��$��3��$	��Top=�$
��Left=�$	��100��$��	0,3,3.6,0q�.+��� $ 
Horizontal=�.+��� -    ��Name$ lblSectCodTitle��$��S_tore :��$��Right=�$	��100�� �� Target ��  �� ElementName$ 
cmbSectCod���� -   	 ��Name$	 
cmbSectCod��$	��300��
 �� 
IsReadOnly
 .  �� SelectedIndex$ 0���� -   $ lblSSirCodTitle��$��
Supp_lier:��$��Right=�$	��110�� �� $ AtxtSsirlst��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -     Name$ AtxtSsirlst��   
SearchType$ C��$
��Left=�$��22��$	��300���� $ 
Horizontal=�.+��� -   $ lblApprovByIdTitle��$��Approve _By:��$��Right=�$	��100�� �� $ AtxtApprovById��  -	   $ AtxtApprovById��$ C��$
��Left=�$��22��$	��300���� -
   $ lblreqRefTitlepr��$��_Reference (if any) :��$��Right=�$	��110�� �� $ txtreqRefpr���  �� .+�(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ txtApprovRef��$��0�� 
 wtxtBx#��[    	Watermark$ Type Referance��$��5,0,0,0q� �� TextWrapping$
 Wrap=�$�� ��$
��Left=�$��22��$	��300���� -    ��Name$ 
btnReferAp��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ 
Horizontal=�.+��� -   $ lblApprovNarTitle��$��_Narration (if any):��$	��Top=�$��Right=�$��25��$	��100�� �� $ txtApprovNar���� -    ��Name$ txtApprovNar��$�� ��$
��Left=�$��50�� J� 	AllowDrop .  �� AcceptsReturn . $	��710��$
 Wrap=��� $ Vertical=�$��2��$��1��$��Bottom=�$��Right=�$��80��.+��� $ 
Horizontal=�.+��� -    ��Name$ chkDateBlocked��$��Approv _Date  : ��|�.  $
��Left=�$��Center=�$	��115��&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtpApprovDat��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��130��{�.  �� $��20��$y�Hidden=��� -   $ btnOk��  StdBtnStyle01#��[ $	��_Ok��$��75���� $ 
Horizontal=�.+��� -   $ lblApprovNoTitle��$��     Approv No :��$
��Left=�$	��115���� -   $ 
lblaprovno��$��PAPMM-XXXXX��$  PAPYYYYMMCCCCXXXXX��$	��130��$
��Bold"�$
��Left=��� $ 
Horizontal=�$��Right=�$��0, -8, 0, 0q�.+��� -   $ chkAutoTransList��$��_Show transactions list��|�.  $��Bottom=��� $��25��$��22��$y�Hidden=��� -   $ 	btnPrint2��$��_Print��$��75��$��22��$��Bottom=�$��12(�$��Verdana)�#��[ �� $��30��$y�Hidden=��� -   $ 	btnUpdate��#��[ $��_Update��$��75���� $��1��$	��Top=�
��� ���$��5��� -   $ gridDetails��$��2��$	��Top=�$y�Visible=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto �.+��� -   $ stkItem��$ Vertical=�$��0��$	��Top=�$��Stretch=�.+��� $ 
Horizontal=�.+��� -   $ lblreqnoTitle��$��Req_uision No :��$��Right=�$	��100�� �� $ cmbreqno���� -   $	 cmbreqno��$	��350��
 . �� $y�Hidden=��� $ 
Horizontal=�.+��� -   $ lblUID��$��Item UI_D Mark:��$	��100��$��Right=� �� $ txtUID��  -   $ txtUID��#��[ $ Manufacturer's Product ID��$��5,0,0,0q�$��22��$	��150��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .    AutoSelectBehavior$ OnFocus=��� -   $ lblRSirCode��$��
Item Code:��$��70��$��Right=� �� $ txtRSirCodepr��  -   $ txtRSirCode��#��[ $ 	Item Code��$��5,0,0,0q� �� 
IsReadOnly . $��22��$	��130�� . $ OnFocus=��� $y�Hidden=��� $ 
Horizontal=�.+��� -   $ lblRSirCodeTitle��$��Descr_iption:��$��Right=�$	��100�� �� $ txtRSirDesc���  �� $��22 �.+�  -    $ txtRSirDesc��$��0��#��[ $ Item Description��$��5,0,0,0q�$��22��$
 Wrap=�$	��350�� . $ OnFocus=��� -!   $ btntxtCross��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ 
Horizontal=�.+��� -"   $ lblApporQtyTitle��$��_Quantity :��$��Right=�$	��100�� �� $ txtApprovQty��  -#   $ txtApprovQty��$��0��#��[ $��Right=�$ Quantity��$��5,0,0,0q�$��22��$
 Wrap=�$	��100�� . $ OnFocus=��� -$   $ lblUnit1��$
��Unit��$��35���� -%   $ lblApprovRatTitle��$��_Rate:��$
��Left=�$��35�� �� $ txtApprovRat��  -&   $ txtApprovRat��$��0��#��[ $
 Rate��$��5,0,0,0q�$��Right=�$��22��$
 Wrap=�$��70�� . $ OnFocus=��� -'   $ 	lblAmount��$��Right=�$��Amount��$	��100��$
��Bold"��� $ 
Horizontal=�.+��� -(   $ lblpaytypeTitle��$��Pa_y Type :��$��Right=�$	��100�� �� $ 
cmbpaytype���� -)   $	 
cmbpaytype��$	��120��
 . .@��� .���� $�� 100% Credit���� .���� $�� 50% Cash + 50% Credit���� .���� $��
 100% Cash���� .���� $�� no Condition���� -*   $ lblApprovNoteTitle��$��N_ote :��$��Right=�$	��Top=�$��25��$��50�� �� $ txtApprovNote���  �� $��22 �.+�  -+   $ txtApprovNote��$��0��#��[ $ Note If Required.�� .  $��22��$
 Wrap=�$	��180�� . $ OnFocus=��� -,   $ btntxtNtCross��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ 
Horizontal=�$
��Left=�.+��� $��15��$y�Hidden=��� $ 
Horizontal=�.+��� --   $ lblListBox1Title��$��_List��$	��255�� �� $ 	ListView1���� -.   $ btnAddAllRecords��#��[ $��Right=�$��Add All Items��$	��Top=�$��22��$	��100���� $y�Hidden=�$��20���� -/   $ btnAddRecord��#��[ $��Right=�$
��_Add��$	��Top=�$��22��$��75���� $ 
Horizontal=�$	��Top=�.+��� -0    ��Name$ 	ListView1��$	��450��$	��300�� �� View �� .P���  �� Width$ 90��$
��Code�� �� DisplayMemberBinding ��  �� Path$ trcode��� $	 260��$��Description�� �� $ trdesc��� $	 100��$
��Unit�� �� $ trunit��� $ 
Horizontal=�$��1��$
��Auto��$	��Top=�
�������$��10,0,0,0q�.+��� -1   $ 	ListView2��$	��471��$	��720�� �� .P��� $ 50��$	��Sl#�� �� $
 trsl��� $	 320�� �� $ trdesc� �� HeaderTemplate 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description of items��$	��320���� $ 80��$	��Qty��  �� CellTemplate    .R�� $��Right=�$	��Top=�$��80��.+��� ! ��Name$! txtBlocLvkQty��$��Right=�$	��Top=����� *
aprovqty+" �� StringFormat$" {0: #,##0.00}���� $ 40��$
��Unit�� �� $ trunit��� $ 90��$
��Rate��    .R�� $��Right=�$	��Top=�$��90��.+��� $! txtBlocLvkRate��$��Right=�$	��Top=����� *	aprovrate+$" {0: #,##0.00}���� $ 90��$��Amount��    .R�� $��Right=�$	��Top=�$��80��.+��� $! txtBlocLvkreqAmt��$��Right=�$	��Top=����� *
aprovamt+$" {0: #,##0.00}���� $ 80��    .R�� $��Right=�$��Center=�$��70��.+��� $ 
Horizontal=�.+��� �� # ��Name$# hlDeleteRow��-2   $ � ToolTip$$ Delete record��
% � Tag% �� $
 trsl�.�.��� $��..\Images\bin1.png��$��16��$��16��$
��Fill=��� $��10��$y�Hidden=��� �� $# 	hlEditRow��-3   $$ Edit record��% �� $
 trsl�.�.��� $��..\Images\edit.png��$��18��$��20��$
��Fill=�� -4   $ gridTransList��$��2��$	��Top=�
�������$y�Hidden=�$��	10,0,10,0q�.+��� $	��Top=�.+��� $ 
Horizontal=�$
��Left=�.+��� $!��Existing Tranasctions List:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� $��10��$y�Hidden=��� -5   $! txtTransTitle��$>��8Transaction Date Range From: 01-Dec-2015 To: 15-Dec-2015��& �� TextAlignment$& Center=�$	��800���� -6   $ lvTransList��$	��350��$��14(�$
��Auto=�$��Hidden=����� ' �� 
TargetType"' ��.��� ( �� Property( � ��) �� Value) �	#FFE4F2E1 �� .P��� $ 60��$	��Sl#��    .R�� $��Right=�$	��Top=�$��50��.+��� $��Right=�$	��Top=�$
��Bold"����� *slnum+�� $�.���� $	 100��$
��Date�� �� $ 	memoDate1��� $	 140��$��Memo No.��$
��Bold"� �� $ memonum1��� $	 250��   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Supplier Name��$	��220��    .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *
ssirname+�� $	 250��   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��
Store Name��$	��220��    .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *
sectname+* � ToolTip* �� *PreparByName+�� $	 250��   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��	Reference��$	��220��    .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *	Referance+* �� *
Naration+�� $	 100��$��Status�� �� $ 
MemoStatus��� $��5��]� .+��� $ 
Horizontal=�$
��Left=�$
��Left=�.+��� $��	Recurring��#��[ $��80��$��22���� $��10��$y�Hidden=��� -7   $ 	chkFilter��+ d� 	IsChecked$+ FalseY�$��Filter Records��$��Center=�$��Center=�� , � AllowsTransparency, . $��Slide=����� $ 	chkFilter��$ 	IsChecked�- � PlacementTarget- �� $ 	chkFilter��.���� $��0,0,5,5q�$��0q�. �� CornerRadius$. 8������ / �� EndPoint$	/ 0,1&�0 �� 
StartPoint$	0 0,1&�.��� 1 � Color$1 	#FFECFBFB��2 � Offset$	2 0.0Q�� $1 	#FFCED9E4��$	2 0.5Q�� $1 	#FF5F6E6E��$	2 1.0Q����� $/ 0.5,1&�$0 0.5,0&�.��� $1 	#FFF7F7F7��$	2 0.0Q�� $1 	#FFCED9E4��$	2 0.5Q�� $1 	#FF22F5F5��$	2 1.0Q�3 J� Effect3 3  -System.Windows.Media.Effects.DropShadowEffect  4   Color$4 Black��5   ShadowDepth$5 5Q�6   
BlurRadius$6 20Q�7   Opacity$	7 0.8Q�8   RenderingBias$8 Quality=�._��� $	��340��$	��140������ $/ 0.5,1&�$0 0.5,0&�.��� $1 	#FFE4F2E1��$2 0.491Q�� $1 	#FF69B28B��$2 0.957Q�z��� -8   theTransform1��.+��� $ Vertical=�.+��� $1��+Filter Option For Getting Transaction List:��$��12(�$��Italic$�$	��340��$��Center=�|�. ���� $/ 0.5,1&�9 � MappingMode$9 RelativeToBoundingBox=�$0 0.5,0&�.��� $1 White��� $1 	#FFB7B7B7��$
2 0.46Q�� $1 	#FFDDDDDD��$2 0.233Q��� $ 
Horizontal=�.+��� $��From :��$��12(�$��Italic$�$��45��$
��Left=�  -9   $ xctk_dtpFromDate��#��[ $��20��$	��130���� $
��To :��$��Italic$�$��28��$��Center=�  -:   $ xctk_dtpToDate��#��[ $��20��$	��130���� $y�Hidden=��� $y�Hidden=��� $ 
Horizontal=�.+��� $��Dept. :��$��45��$
��Left=��� -;   $	 cmbfDept��$	��288��: �� SelectedValue$	: Tag���� -<   $ 	btnFilter��$��	Go Filter��$��20��$��60��$��12(�$��Verdana)�#��[ ; �� Left$	; 155��
< �� Top$	< 105���� $��30��$y�Hidden=��� $ 
Horizontal=�$��Right=�$��Right=�.+��� $��10��$y�Hidden=��� $
��Edit��#��[ $��80��$��22���� $��10��$y�Hidden=��� $��Delete��#��[ $��80��$��22���� $��10��$y�Hidden=��� -=   $ 	chkPrint2��$+ FalseY�$��Print Memo/List��$��Center=�$��Center=�� , . $��Slide=����� $ 	chkPrint2��$ 	IsChecked�- �� $ 	chkPrint2��.���� $	��134��$	��146������ $/ 0.5,1&�$0 0.5,0&�.��� $1 	#FFE4F2E1��$2 0.491Q�� $1 	#FF69B28B��$2 0.957Q�z��� ->   theTransform2��.+��� $ Vertical=�$	��134��.+��� $��Print Options��$��12(�$��Italic$�$
��Bold"�
���**��$
��Left=��� $ Vertical=�$��10,0,0,0q�.+�	� -?   = 	�Name$= rb3SelectedMemo��$��Selected Memo��$
+ TrueY��� $y�Hidden=�	� -@   $= rb3TableRecoreds��$��Table Records���� �� $ Vertical=�$��10,2,0,0q�.+�	� -A   $= rb3QuickPrint��$��Quick Print���� $y�Hidden=�	� -B   $= rb3PrintPreview��$��Print Preview��$
+ TrueY��� -C   $ 	btnPrint3��$��Go Print��$��20��$��60��$��12(�$��Verdana)�#��[ $; 34��$	< 116���� $��20��$y�Hidden=�