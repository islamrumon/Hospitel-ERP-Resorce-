   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  .    (ASITHmsWpf.Inventory.frmEntryStoreIssue1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  -   
  � Tag$0  *Item Issue/Consumption - Entry/Edit Screen��   IOUserControl#��[  .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� -    �Name$ gridMastersi��$��0��$	��Top=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��245* ��� $
��174* ��� $
��181* �.+��� $��0��$��0��$
��Left=�.+��� $��New/Selected Tranasction:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� -    ��Name$ stkIntro�� �� Orientation$ Vertical=�$��0��$��2��$��1��$��3��$	��Top=�$
��Left=�$	��100��$��	0,3,3.6,0q�.+��� $ 
Horizontal=�.+��� -    ��Name$ 
lblsectcod��$��Issue _From :��$��Right=�$	��100�� �� Target ��  �� ElementName$ 
cmbSectCod���� -   	 ��Name$	 
cmbSectCod��$	��300��
 �� 
IsReadOnly
 .  �� SelectedIndex$ 0���� -   $ lblsectCode2Title��$��Issue _To :��$��Right=�$	��110�� �� $ cmbSectCod2���� -   $	 cmbSectCod2��$	��300��
 . $ 0���� $ 
Horizontal=�.+��� -   $ lblSirByidTitle��$��Issued _By :��$��Right=�$	��100�� �� $ AtxtSirById��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -	     Name$ AtxtSirById��   
SearchType$ C��$
��Left=�$��22��$	��300���� -
   $ lblrecvbyidTitle��$��Recei_ved By :��$��Right=�$	��110�� �� $ Atxtrecvbyid��  -   $ Atxtrecvbyid��$ C��$
��Left=�$��22��$	��300���� $ 
Horizontal=�.+��� -   $ lblSrfNarTitle��$��_Narration (if any):��$	��Top=�$��Right=�$��25��$	��100�� �� $ 	txtSrfNar���� -    ��Name$ 	txtSrfNar��$�� ��$
��Left=�$��45�� J� 	AllowDrop .  �� AcceptsReturn . $	��300�� �� TextWrapping$
 Wrap=��� -   $ lblsirrefTitle��$��_Reference (if any):��$��Right=�$	��Top=�$	��110��$��42�� �� $ txtissRefpr���  �� $��22 �.+�(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ txtissRefpr��$	��Top=�$��0�� 
 wtxtBx#��[    	Watermark$ Type Referance��$��5,0,0,0q�$
 Wrap=�$�� ��$
��Left=�$��22��$	��299���� -    ��Name$ 
btnReferpr��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ Vertical=�$��2��$��1��$��Bottom=�$��Right=�$	��100��.+��� $ 
Horizontal=�.+��� -    ��Name$ chkDateBlocked��$��Issue _Date : ��|�.  $
��Left=�$��Center=�$	��115��&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtpSrfDat��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��130��{�.  �� $��20��$y�Hidden=��� -   $ btnOk��  StdBtnStyle01#��[ $	��_Ok��$��75���� $ 
Horizontal=�.+��� -   $ lblSrfNoTitle��$��
S.I.R. No:��$��Right=�$	��115���� -   $ lblSrfNo��$��SIRMM-XXXXX��$  SIRYYYYMMCCCCXXXXX��$	��130��$
��Bold"�$
��Left=��� $ 
Horizontal=�$
��Left=�$��0, -3, 0, 0q�$��25��.+��� -   $ chkAutoTransList��$��_Show transactions��|�.  $��Bottom=��� $��50��$y�Hidden=��� -   $ 	btnPrint2��$��_Print��$��75��$��Bottom=�$��12(�$��Verdana)�#��[ �� $��25��$y�Hidden=��� -   $ 	btnUpdate��#��[ $��_Update��$��75���� $��1��$	��Top=�
��� ���$��5��� -   $ gridDetails��$��2��$	��Top=�$y�Visible=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto �.+��� -   $ stkItem��$ Vertical=�$��0��$	��Top=�$��Stretch=�.+��� -   $ stkpReqList��$ 
Horizontal=�.+��� -   $ 
lblSrfList��$��_Requisition List :��$��Right=�$	��100�� �� $ 
cmbSrfList���� -   $	 
cmbSrfList��$	��350��
 . $ 0���� $��2��$y�Hidden=��� $ 
Horizontal=�.+��� -   $ lblUID��$��Item UI_D Mark:��$	��100��$��Right=� �� $ txtUID��  -   $ txtUID��#��[ $ Manufacturer's Product ID��$��5,0,0,0q�$��22��$	��150��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .    AutoSelectBehavior$ OnFocus=��� $ 
Horizontal=�.+��� -    $ lblRSirCodeTitle��$��_Item Code & Name:��$��Right=�$	��115�� �� $ txtRSirDesc���� $ 
Horizontal=�.+�  -!   $ txtRSirCode��#��[ $ 	Item Code��$��5,0,0,0q�$��22��$	��100�� . $ OnFocus=� �� 
IsReadOnly . �  �� $
��Auto �.+�  -"   $ txtRSirDesc��$��0��#��[ $ Item Description��$��5,0,0,0q�$��22��$
 Wrap=�$	��350�� . $ OnFocus=��� -#   $ btntxtRSirDesc��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ 
Horizontal=�.+��� -$   $ lblStkBalDes��$�� ��$��Right=�$	��180���� -%   $ lblSrfQtyTitle��$��Issue _Qty:��$��Right=�$��60�� �� $ 	txtSirQty��  -&   $ 	txtSirQty��$��0�� � 
InputScope$ Number�� �� TextAlignment$ Right=�#��[ $ Issue Quantity��$��5,0,0,0q�$��22��$
 Wrap=�$��80�� . $ OnFocus=��� -'   $ lblUnit1��$
��Unit��$��50���� -(   $ btnAddRecord��#��[ $
��_Add��$	��Top=�$��22��$��75���� $ 
Horizontal=�.+��� -)   $ lblListBox1Title��$��_List��$��80�� �� $ 	ListView1���� $ 
Horizontal=�$	��Top=�.+��� -*    ��Name$ 	ListView1��$��Bottom=�$	��450��$	��285�� �� View �� .P���  �� Width$	 410��$��Description�� �� DisplayMemberBinding ��   �� Path$  trdesc1��� $ 
Horizontal=�$��1��$
��Auto��$	��Top=�
�������$��10,0,0,0q�.+��� -+   $ ListView2pr��$	��445��$	��721��$
��Auto=�$��Hidden=����� ! �� 
TargetType"! ��.��� " �� Property" � ��# �� Value# �	#FFE4F2E1 �� .P��� $ 40��$	��Sl#�� �� $
  trsl��� $	 450�� �� $  trdesc�$ �� HeaderTemplate$ 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description of items��$	��330���� $	 100��$	��Qty��% �� CellTemplate%   .R�� $��Center=�$	��Top=�$��80��.+��� & ��Name$& txtBlocLvkreqQty��$��Center=�$	��Top=�$
��Bold"����� *sirqty+' �� StringFormat$' {0: #,##0.00}���� $ 60��$
��Unit�� �� $  trunit��� $ 80��%   .R�� $��Right=�$��Center=�$��70��.+��� $ 
Horizontal=�.+��� �� ( ��Name$( hlDeleteRow��-,   ) � ToolTip$) Delete record��
* � Tag* �� $
  trsl�.�.��� $��..\Images\bin1.png��$��16��$��16��$
��Fill=��� $��10��$y�Hidden=��� �� $( 	hlEditRow��--   $) Edit record��* �� $
  trsl�.�.��� $��..\Images\edit.png��$��18��$��20��$
��Fill=�� -.   $ gridTransList��$��2��$	��Top=�
�������$y�Hidden=�$��	10,0,10,0q�.+��� $	��Top=�.+��� $ 
Horizontal=�$
��Left=�.+��� $!��Existing Tranasctions List:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� $��10��$y�Hidden=��� -/   $& txtTransTitle��$>��8Transaction Date Range From: 01-Dec-2015 To: 15-Dec-2015��+ �� TextAlignment$+ Center=�$	��800���� -0   $ lvTransListpr��$	��350��$��14(�$
��Auto=�$��Hidden=����� "! ��.��� " � ��# �	#FFE4F2E1 �� .P��� $ 60��$	��Sl#��%   .R�� $��Right=�$	��Top=�$��50��.+��� $��Right=�$	��Top=�$
��Bold"����� *slnum+�� $�.���� $	 100��$
��Date�� �� $  	memoDate1��� $	 150��$��Memo No.�� �� $  memonum1��� $	 200��$   .R�� $��Center=�$	��Top=�.+��� $��Center=�$	��Top=�$��
Issue From��$	��190��%   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *
sectname+, � ToolTip, �� *
recvbyName+�� $	 200�� �� $  	sectname2�$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Issue To��$	��190���� $	 200�� �� $  
recvbyName�$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Receeived By��$	��190���� $	 192��$   .R�� $��Center=�$	��Top=�.+��� $��Center=�$	��Top=�$��	Reference��$	��190��%   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *	Referance+, �� *
Naration+�� $	 100��$��Status�� �� $  
MemoStatus��� $��5��]� .+��� $ 
Horizontal=�$
��Left=�$
��Left=�.+��� $��	Recurring��#��[ $��80��$��22���� $��10��$y�Hidden=��� -1   $ 	chkFilter��- d� 	IsChecked$- FalseY�$��Filter Records��$��Center=�$��Center=�� . � AllowsTransparency. . $��Slide=����� $ 	chkFilter��$  	IsChecked�/ � PlacementTarget/ �� $ 	chkFilter��.���� $��0,0,5,5q�$��0q�0 �� CornerRadius$0 8������ 1 �� EndPoint$	1 0,1&�2 �� 
StartPoint$	2 0,1&�.��� 3 � Color$3 	#FFECFBFB��4 � Offset$	4 0.0Q�� $3 	#FFCED9E4��$	4 0.5Q�� $3 	#FF5F6E6E��$	4 1.0Q����� $1 0.5,1&�$2 0.5,0&�.��� $3 	#FFF7F7F7��$	4 0.0Q�� $3 	#FFCED9E4��$	4 0.5Q�� $3 	#FF22F5F5��$	4 1.0Q�5 J� Effect5 3  -System.Windows.Media.Effects.DropShadowEffect  6   Color$6 Black��7   ShadowDepth$7 5Q�8   
BlurRadius$8 20Q�9   Opacity$	9 0.8Q�:   RenderingBias$: Quality=�._��� $	��340��$	��160������ $1 0.5,1&�$2 0.5,0&�.��� $3 	#FFE4F2E1��$4 0.491Q�� $3 	#FF69B28B��$4 0.957Q�z��� -2   theTransform1��.+��� $ Vertical=�.+��� $1��+Filter Option For Getting Transaction List:��$��12(�$��Italic$�$	��340��$��Center=�|�. ���� $1 0.5,1&�; � MappingMode$; RelativeToBoundingBox=�$2 0.5,0&�.��� $3 White��� $3 	#FFB7B7B7��$
4 0.46Q�� $3 	#FFDDDDDD��$4 0.233Q��� $ 
Horizontal=�.+��� $��From :��$��12(�$��Italic$�$��45��$
��Left=�  -3   $ xctk_dtpFromDate��#��[ $��20��$	��130���� $
��To :��$��Italic$�$��28��$��Center=�  -4   $ xctk_dtpToDate��#��[ $��20��$	��130���� $y�Hidden=��� $ 
Horizontal=�.+��� $��Iss. Fr:��$
��Left=�$��45���� -5   $	 cmbfIsf��$	��288��< �� SelectedValue$	< Tag���� $y�Hidden=��� $ 
Horizontal=�.+��� $��Iss. To:��$��45��$
��Left=��� -6   $	 cmbfIst��$	��288��$	< Tag���� -7   $ 	btnFilter��$��	Go Filter��$��20��$��60��$��12(�$��Verdana)�#��[ = �� Left$	= 155��
> �� Top$	> 125���� $��30��$y�Hidden=��� $ 
Horizontal=�$��Right=�$��Right=�.+��� $��10��$y�Hidden=��� $
��Edit��#��[ $��80��$��22���� $��10��$y�Hidden=��� $��Delete��#��[ $��80��$��22���� $��10��$y�Hidden=��� -8   $ 	chkPrint2��$- FalseY�$��Print Memo/List��$��Center=�$��Center=�� . . $��Slide=����� $ 	chkPrint2��$  	IsChecked�/ �� $ 	chkPrint2��.���� $	��134��$	��146������ $1 0.5,1&�$2 0.5,0&�.��� $3 	#FFE4F2E1��$4 0.491Q�� $3 	#FF69B28B��$4 0.957Q�z��� -9   theTransform2��.+��� $ Vertical=�$	��134��.+��� $��Print Options��$��12(�$��Italic$�$
��Bold"�
���**��$
��Left=��� $ Vertical=�$��10,0,0,0q�.+�	� -:   ? 	�Name$? rb3SelectedMemopr��$��Selected Memo��$
- TrueY��� $y�Hidden=�	� -;   $? rb3TableRecoredspr��$��Table Records��$- FalseY��� �� $ Vertical=�$��10,2,0,0q�.+�	� -<   $? rb3QuickPrintpr��$��Quick Print��$- FalseY��� $y�Hidden=�	� -=   $? rb3PrintPreviewpr��$��Print Preview��$
- TrueY��� ->   $ 	btnPrint3��$��Go Print��$��20��$��60��$��12(�$��Verdana)�#��[ $= 34��$	> 116���� $��20��$y�Hidden=�