   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:ASITHmsWpf;assembly=ASITHmsWpf
ASITHmsWpf  A%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  ,    &ASITHmsWpf.Inventory.frmEntryStoreReq1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     @General,clr-namespace:ASITHmsWpf;assembly=ASITHmsWpf     4UC%clr-namespace:ASITHmsWpf.UserControls     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -   
  � Tag$A  ;Item Requisition By Department To Store - Entry/Edit Screen��   IOUserControl#��[  .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� -    �Name$ 
gridMaster��$��0��$	��Top=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��245* ��� $
��174* ��� $
��181* �.+��� $��0��$��0��$
��Left=�.+��� $��New/Selected Tranasction:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� -    ��Name$ stkIntro�� �� Orientation$ Vertical=�$��0��$��2��$��1��$��3��$	��Top=�$
��Left=�$	��100��$��	0,3,3.6,0q�.+��� $ 
Horizontal=�.+��� -    ��Name$ lblSectCodTitle��$��Requisition _From :��$��Right=�$	��100�� �� Target ��  �� ElementName$ 
cmbSectCod���� -   	 ��Name$	 
cmbSectCod��
�������$	��300��
 �� 
IsReadOnly
 .  �� SelectedIndex$ 0���� -   $ lblSectCod2Title��$��(Req._To) Store:��$��Right=�$	��110�� �� $ cmbSectCod2���� -   $	 cmbSectCod2��$	��300��
�������
 . $ 0���� $ 
Horizontal=�.+��� -   $ lblSrfByIdTitle��$��Requisition _By:��$��Right=�$	��100�� �� $ AtxtSrfById��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -	     Name$ AtxtSrfById��   
SearchType$ C��$
��Left=�$��22��$	��300���� -
   $ lblSrfRefTitle��$��_Reference (if any):��$��Right=�$	��110�� �� $ 	txtSrfRef���  �� .+�(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ 	txtSrfRef��$��0�� 
 wtxtBx#��[    	Watermark$ Referance (if any)��$��5,0,0,0q�$��22�� �� TextWrapping$
 Wrap=�$	��300��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .    AutoSelectBehavior$ OnFocus=��� -    ��Name$ btntxtSrfRef��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ 
Horizontal=�.+��� -   $ lblSrfNarTitle��$��_Narration (if any):��$	��Top=�$��Right=�$��25��$	��100�� �� $ 	txtSrfNar���� -    ��Name$ 	txtSrfNar��$�� ��$
��Left=�$��50�� J� 	AllowDrop .  �� AcceptsReturn . $	��710��$
 Wrap=��� $ Vertical=�$��2��$��1��$��Bottom=�$��Right=�$��80��.+��� $ 
Horizontal=�.+��� -    ��Name$ chkDateBlocked��$��Requision _Date : ��|�.  $��Right=�$��Center=�$	��115��&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtpSrfDat��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��130��{�.  �� $��20��$y�Hidden=��� -   $ btnOk��  StdBtnStyle01#��[ $	��_Ok��$��75���� $ 
Horizontal=�.+��� -   $ lblSrfNoTitle��$��
S.R.F. No:��$��Right=�$	��115���� -   $ lblSrfNo��$��SRQMM-CCCC-XXXXX��$  SRQYYYYMMCCCCXXXXX��$	��140��$
��Bold"�$
��Left=��� $ 
Horizontal=�$��Right=�$��0, -8, 0, 0q�.+��� -   $ chkAutoTransList��$��_Show transactions list��|�.  $��Bottom=��� $��35��$��22��$y�Hidden=��� -   $ 	btnPrint2��$��_Print��$��75��$��Bottom=�$��12(�$��Verdana)�#��[ �� $��20��$y�Hidden=��� -   $ 	btnUpdate��#��[ $��_Update��$��75���� $��1��$	��Top=�
��� ���$��5��� -   $ gridDetails��$��2��$	��Top=�$y�Visible=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto �.+��� -   $ stkItem��$ Vertical=�$��0��$	��Top=�$��Stretch=�.+��� $ 
Horizontal=�.+��� -   $ lblUID��$��Item UI_D Mark:��$	��100��$��Right=� �� $ txtUID��  -   $ txtUID��#��[ $ Manufacturer's Product ID��$��5,0,0,0q�$��22��$	��130�� . $ OnFocus=��� -   $ lblRSirCode��$��_Item Code:��$��70��$��Right=� �� $ txtRSirDesc��  -   $ txtRSirCode��#��[ $ 	Item Code��$��5,0,0,0q�|�.  $��22��$	��130�� . $ OnFocus=� �� 
IsReadOnly . �� -   $ 
btnAddCode��$��_+��$��20��$��22��|�.  �� $��1��$y�Hidden=��� $ 
Horizontal=�.+��� -   $ lblRSirCodeTitle��$��Description:��$��Right=�$	��100��$��24�� �� $ txtRSirDesc���  �� $
��291* ��� $	��59* � �� $��24 �.+�  -   $ txtRSirDesc��$��0��#��[ $ Item Description��$��5,0,0,0q�$��24��$
 Wrap=�$	��330�� . $ OnFocus=�$	��Top=�$��2���� -    $ btntxtRSirDesc��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=�$��1��$	��0,2q��� -!   $ btnRefreshCode��$��_f��$��20��$��22��|�.  �� $ 
Horizontal=��� $��1��$y�Hidden=��� $ 
Horizontal=�.+��� -"   $ lblSrfQtyTitle��$��
_Quantity:��$��Right=�$	��100�� �� $ 	txtSrfQty��  -#   $ 	txtSrfQty��$��0�� �� TextAlignment$ Right=�#��[ $ Req. Qty��$��5,0,0,0q�$��22��$
 Wrap=�$��80�� . $ OnFocus=��� -$   $ lblUnit1��$
��Unit��$	��100���� $��95��$y�Hidden=��� -%   $ btnAddRecord��#��[ $
��_Add��$	��Top=�$��22��$��75���� $ Vertical=�$	��Top=�.+��� -&   $ lblListBox1Title��$��_List��$��80��$
��Left=� �� $ 	ListView1���� -'    ��Name$ 	ListView1��$	��450��$	��335�� �� View �� .P���  �� Width$	 100��$
��Code�� �� DisplayMemberBinding ��  �� Path$ trcode��� $	 280�� �� $ trdesc�  �� HeaderTemplate  5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description��$	��250���� $ 40��$
��Unit�� �� $ trunit��� $ 
Horizontal=�$��1��$
��Auto��$	��Top=�
�������$��10,0,0,0q�.+��� -(   $ ListView2sr��$	��450��$	��721��$��14(�$
��Auto=�$��Hidden=����� ! �� 
TargetType"! ��.��� " �� Property" � ��# �� Value# �	#FFE4F2E1 �� .P��� $ 50��$	��Sl#��$ �� CellTemplate$   .R�� $��Right=�$	��Top=�$��50��.+��� $��Right=�$	��Top=����� *trsl+�� $	 410�� �� $ trdesc�    .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description of items��$	��430���� $	 120��$��Quantaty��$   .R�� $��Right=�$	��Top=�$	��100��.+��� % ��Name$% txtBlocLvkSrfQty��$��Right=�$	��Top=�$
��Bold"����� *srfqty+& �� StringFormat$& {0: #,##0.00}���� $ 60��$
��Unit�� �� $ trunit��� $ 80��$   .R�� $��Right=�$��Center=�$��70��.+��� $ 
Horizontal=�.+��� �� ' ��Name$' hlDeleteRow��-)   ( � ToolTip$( Delete record��
) � Tag) �� $
 trsl�.�.��� $��..\Images\bin1.png��$��16��$��16��$
��Fill=��� $��10��$y�Hidden=��� �� $' 	hlEditRow��-*   $( Edit record��) �� $
 trsl�.�.��� $��..\Images\edit.png��$��18��$��20��$
��Fill=�� -+   $ gridTransList��$��2��$	��Top=�$y�Hidden=�
�������$��	10,0,10,0q�.+��� $ Vertical=�$	��Top=�$	��450��.+��� $ 
Horizontal=�$
��Left=�.+��� $!��Existing Tranasctions List:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� $��10��$y�Hidden=��� -,   $% txtTransTitle��$>��8Transaction Date Range From: 01-Dec-2015 To: 15-Dec-2015��$
��Bold"�* �� TextAlignment$* Center=�$	��800���� --   $ lvTransList��$	��350��$��14(�$
��Auto=�$��Hidden=����� "! ��.��� " � ��# �	#FFE4F2E1 �� .P��� $ 60��$	��Sl#��$   .R�� $��Right=�$	��Top=�$��50��.+��� $��Right=�$	��Top=�$
��Bold"����� *slnum+�� $�.���� $	 100��$
��Date�� �� $ 	memoDate1��� $	 140��$��Memo No.��$
��Bold"� �� $ memonum1��� $	 250��    .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Department Name��$	��220��$   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *
sectname++ � ToolTip+ �� *PreparByName+�� $	 250�� �� $ 	sectname2�    .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��
Store Name��$	��220���� $	 250��    .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��	Reference��$	��220��$   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *	Referance++ �� *
Naration+�� $	 100��$��Status�� �� $ 
MemoStatus��� $��5��]� .+��� $ 
Horizontal=�$
��Left=�$
��Left=�.+��� $��	Recurring��#��[ $��80��$��22��{�.  �� $��10��$y�Hidden=��� -.   $ 	chkFilter��, d� 	IsChecked$, FalseY�$��Filter Records��$��Center=�$��Center=�� - � AllowsTransparency- . $��Slide=����� $ 	chkFilter��$ 	IsChecked�. � PlacementTarget. �� $ 	chkFilter��.���� $��0,0,5,5q�$��0q�/ �� CornerRadius$/ 8������ 0 �� EndPoint$	0 0,1&�1 �� 
StartPoint$	1 0,1&�.��� 2 � Color$2 	#FFECFBFB��3 � Offset$	3 0.0Q�� $2 	#FFCED9E4��$	3 0.5Q�� $2 	#FF5F6E6E��$	3 1.0Q����� $0 0.5,1&�$1 0.5,0&�.��� $2 	#FFF7F7F7��$	3 0.0Q�� $2 	#FFCED9E4��$	3 0.5Q�� $2 	#FF22F5F5��$	3 1.0Q�4 J� Effect4 3  -System.Windows.Media.Effects.DropShadowEffect  5   Color$5 Black��6   ShadowDepth$6 5Q�7   
BlurRadius$7 20Q�8   Opacity$	8 0.8Q�9   RenderingBias$9 Quality=�._��� $	��340��$	��160������ $0 0.5,1&�$1 0.5,0&�.��� $2 	#FFE4F2E1��$3 0.491Q�� $2 	#FF69B28B��$3 0.957Q�z��� -/   theTransform1��.+��� $ Vertical=�.+��� $1��+Filter Option For Getting Transaction List:��$��12(�$��Italic$�$	��340��$��Center=�|�. ���� $0 0.5,1&�: � MappingMode$: RelativeToBoundingBox=�$1 0.5,0&�.��� $2 White��� $2 	#FFB7B7B7��$
3 0.46Q�� $2 	#FFDDDDDD��$3 0.233Q��� $ 
Horizontal=�.+��� $��From :��$��12(�$��Italic$�$��45��$
��Left=�  -0   $ xctk_dtpFromDate��#��[ $��20��$	��130���� $
��To :��$��Italic$�$��28��$��Center=�  -1   $ xctk_dtpToDate��#��[ $��20��$	��130���� $y�Hidden=��� $ 
Horizontal=�.+��� $��Dept. :��$
��Left=�$��45���� -2   $	 cmbFDtNm��$	��288��; �� SelectedValue$	; Tag���� $y�Hidden=��� $ 
Horizontal=�.+��� $��Store :��$��45��$
��Left=��� -3   $	 cmbfStNm��$	��288��$	; Tag���� -4   $ 	btnFilter��$��	Go Filter��$��20��$��60��$��12(�$��Verdana)�#��[ < �� Left$	< 155��
= �� Top$	= 125���� $��30��$y�Hidden=��� $ 
Horizontal=�$��Right=�$��Right=�.+��� $��10��$y�Hidden=��� $
��Edit��#��[ $��80��$��22���� $��10��$y�Hidden=��� $��Delete��#��[ $��80��$��22���� $��10��$y�Hidden=��� -5   $ 	chkPrint2��$, FalseY�$��Print Memo/List��$��Center=�$��Center=�� - . $��Slide=����� $ 	chkPrint2��$ 	IsChecked�. �� $ 	chkPrint2��.���� $��0,0,5,5q�$��0q�$/ 8������ $	0 0,1&�$	1 0,1&�.��� $2 	#FFECFBFB��$	3 0.0Q�� $2 	#FFCED9E4��$	3 0.5Q�� $2 	#FF5F6E6E��$	3 1.0Q����� $0 0.5,1&�$1 0.5,0&�.��� $2 	#FFF7F7F7��$	3 0.0Q�� $2 	#FFCED9E4��$	3 0.5Q�� $2 	#FF22F5F5��$	3 1.0Q�4   $5 Black��$6 5Q�$7 20Q�$	8 0.8Q�$9 Quality=�._��� $	��134��$	��146������ $0 0.5,1&�$1 0.5,0&�.��� $2 	#FFE4F2E1��$3 0.491Q�� $2 	#FF69B28B��$3 0.957Q�z��� -6   theTransform2��.+��� $ Vertical=�$	��134��.+��� $��Print Options��$��12(�$��Italic$�$
��Bold"�
���**��$
��Left=��� $ Vertical=�$��10,0,0,0q�.+�	� -7   > 	�Name$> rb3SelectedMemo��$��Selected Memo��$
, TrueY��� $y�Hidden=�	� -8   $> rb3TableRecoreds��$��Table Records���� �� $ Vertical=�$��10,2,0,0q�.+�	� -9   $> rb3QuickPrint��$��Quick Print���� $y�Hidden=�	� -:   $> rb3PrintPreview��$��Print Preview��$
, TrueY��� -;   $ 	btnPrint3��$��Go Print��$��20��$��60��$��12(�$��Verdana)�#��[ $< 34��$	= 116���� $��20��$y�Hidden=�