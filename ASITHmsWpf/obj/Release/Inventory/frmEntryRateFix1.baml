   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  +    %ASITHmsWpf.Inventory.frmEntryRateFix1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  -   
  � Tag$<  6Quotation Receive/CS/Rate Fixation - Entry/Edit Screen��   IOUserControl#��[  .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� -    �Name$ gridMasterpr��$��0��$	��Top=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��245* ��� $
��174* ��� $
��181* �.+��� $��0��$��0��$
��Left=�.+��� $��New/Selected Tranasction:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� -    ��Name$ 
stkIntropr�� �� Orientation$ Vertical=�$��0��$��2��$��1��$��3��$	��Top=�$
��Left=�$	��100��$��	0,3,3.6,0q�.+��� $ 
Horizontal=�.+��� -    ��Name$ lblSectCodTitlepr��$��	 S_tore :��$��Right=�$	��100�� �� Target ��  �� ElementName$ cmbSectCodpr���� -   	 ��Name$	 cmbSectCodpr��$	��300��
 �� 
IsReadOnly
 .  �� SelectedIndex$ 0���� -   $ lblssircode��$�� S_upplier :��$��Right=�$	��108�� �� $ cmbssircode��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -     Name$ Atxtssircode��   
SearchType$ C��$
��Left=�$��22��$	��300���� $ 
Horizontal=�.+��� -   $ lblapproveById��$��Approved _By :��$��Right=�$	��100�� �� $ AtxtapproveById��  -	   $ AtxtapproveById��$ C��$
��Left=�$��22��$	��300���� -
   $ lbqrqRefTitlepr��$��_Reference (if any) :��$��Right=�$	��110�� �� $ 
txtqrRefpr���  �� .+�(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ 
txtqrRefpr��$��0�� 
 wtxtBx#��[    	Watermark$ Type Referance��$��5,0,0,0q� �� TextWrapping$
 Wrap=�$�� ��$
��Left=�$��22��$	��300���� -    ��Name$ 
btnReferpr��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ 
Horizontal=�.+��� -   $ lblqrNarTitlepr��$��Narrat_ion(if any):��$	��Top=�$��Right=�$��25��$	��100�� �� $ 
txtqrNarpr���� -    ��Name$ 
txtqrNarpr��$�� ��$
��Left=�$��50�� J� 	AllowDrop .  �� AcceptsReturn . $	��710��$
 Wrap=��� $ Vertical=�$��2��$��1��$��Bottom=�$��Right=�$��80��.+��� $ 
Horizontal=�.+��� -    ��Name$ chkDateBlocked��$��Approval _Date : ��|�.  $��Right=�$��Center=�$	��115��&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtpreqDatpr��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��130��{�.  �� $��20��$y�Hidden=��� -   $ btnOkpr��  StdBtnStyle01#��[ $	��_Ok��$��75���� $ 
Horizontal=�.+��� -   $ lblreqNoTitlepr��$��Approval No:��$��Right=�$	��115���� -   $ 
lblreqNopr��$��QRAMM-XXXXX��$  QRAYYYYMMCCCCXXXXX��$	��130��$
��Bold"�$
��Left=��� $ 
Horizontal=�$��Right=�$��0, -8, 0, 0q�.+��� -   $ chkAutoTransListpr��$��_Show transactions list��|�.  $��Bottom=��� $��40��$��22��$y�Hidden=��� -   $ btnPrint2pr��$��_Print��$��70��$��22��$��Bottom=�$��12(�$��Verdana)�#��[ �� $��20��$y�Hidden=��� -   $ btnUpdatepr��#��[ $��_Update��$��75���� $��1��$	��Top=�
��� ���$��5��� -   $ gridDetailspr��$��2��$	��Top=�$y�Visible=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto �.+��� -   $ 	stkItempr��$ Vertical=�$��0��$	��Top=�$��Stretch=�.+��� $ 
Horizontal=�.+��� -   $ lblRSirCodeTitlepr��$ ��_Item Code & Description :��$��Right=� �� $ txtRSirDescpr���� $ 
Horizontal=�.+�  -   $ txtRSirCodepr��#��[ $ 	Item Code��$��5,0,0,0q�$�� ��$
��Left=�$��22��$	��100�� �� 
IsReadOnly . �  �� .+�  -   $ txtRSirDescpr��$��0��#��[ $ Item Description��$��5,0,0,0q�$��22��$
 Wrap=�$	��350��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .    AutoSelectBehavior$ OnFocus=��� -   $ btntxtRSirDescpr��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ 
Horizontal=�$	��450��.+��� -   $ lblexpirydat��$��_Expire Date :��$
��Left=�$��80�� �� $ xctk_dtprexpire��  -   $ xctk_dtprexpire��#��[ $
��Bold"�$��22��$	��120���� -   $ 	lblRatepr��$��_Rate :��$
��Left=�$��38�� �� $ txtrqRatepr��  -    $ txtrqRatepr�� �� TextAlignment$ Right=�$��0��$
 Wrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $ 
Type Rate.��$�� ��$
��Left=�$��22��$��70���� $��Unit: ���� -!   $ 
lblUnit1pr��$��50���� $ 
Horizontal=�$	��450��.+��� -"   $ lblaprvnote��$��App_v. Note :��$
��Left=�$��80�� �� $ txtaprvnote���  �� .+�  -#   $ txtaprvnote��$��0��#��[ $ 	Type Note��$��5,0,0,0q�$
 Wrap=�$�� ��$
��Left=�$��22��$	��290���� -$   $ btnaprvnote��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� -%   $ btnAddRecordpr��#��[ $
��_Add��$	��Top=�$��Right=�$��22��$��75���� $ 
Horizontal=�.+��� -&   $ lblListBox1Title��$��_List :��$��80�� �� $ ListView1pr���� $	��295��$y�Hidden=��� $ 
Horizontal=�$	��Top=�.+��� -'    ��Name$ ListView1pr��$	��450��$	��278�� �� View �� .P���  �� Width$	 100��$
��Code�� �� DisplayMemberBinding ��  �� Path$ trcode��� $	 320��$��Description�� �� $ trdesc��� $ 
Horizontal=�$��1��$
��Auto��$	��Top=�
�������$��10,0,0,0q�.+��� -(   $ ListView2pr��$	��450��$	��721��$��14(�$
��Auto=�$��Hidden=�����   �� 
TargetType"  ��.��� ! �� Property! � ��" �� Value" �	#FFE4F2E1 �� .P��� $ 50��$	��Sl#��# �� CellTemplate# 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $��Right=�$	��Top=�$��50��.+��� $��Right=�$	��Top=����� *trsl+�� $	 300�� �� $ trdesc�$ �� HeaderTemplate$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description of items��$	��430���� $ 80��$
��Rate��#   .R�� $��Right=�$	��Top=�$��80��.+��� % ��Name$% txtBlocLvkreqRate��$��Right=�$	��Top=����� *
aprvrate+& �� StringFormat$& {0: #,##0.00}���� $ 90��$��
Expr. Date��#   .R�� $��Right=�$	��Top=�$��80��.+��� $% txtBlocLvkreqAmt��$��Right=�$	��Top=����� *
expiryDate+�� $ 80��#   .R�� $��Right=�$��Center=�$��70��.+��� $ 
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
����� �$	��180���� $��10��$y�Hidden=��� $>��8Transaction Date Range From: 01-Dec-2015 To: 15-Dec-2015��* �� TextAlignment$* Center=�$	��800���� -,   $ lvTransList��$	��350��$��14(�$
��Auto=�$��Hidden=����� "  ��.��� ! � ��" �	#FFE4F2E1 �� .P��� $ 60��$	��Sl#��#   .R�� $��Right=�$	��Top=�$��50��.+��� $��Right=�$	��Top=�$
��Bold"����� *slnum+�� $�.���� $	 100��$
��Date�� �� $ 	memodate1��� $	 140��$��Memo No.��$
��Bold"� �� $ memonum1��� $	 190��$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��
Store Name��$	��190��#   .R�� $
��Left=�$	��Top=�$	��190��.+��� $
��Left=�$	��Top=����� *
sectname++ � ToolTip+ �� *approvbyName+�� $	 190�� �� $ ssirname�$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Supplier Name��$	��180���� $	 230�� �� $ approvbyName�$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��
Approve by��$	��230���� $	 150��$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��	Reference��$	��150��#   .R�� $
��Left=�$	��Top=�$	��150��.+��� $
��Left=�$	��Top=����� *	Referance++ �� *
Naration+�� $	 100��$��Status�� �� $ 
MemoStatus��� $��5��]� .+��� $ 
Horizontal=�$
��Left=�$
��Left=�.+��� $��	Recurring��#��[ $��80��$��22���� $��10��$y�Hidden=��� --   $ 	chkFilter��, d� 	IsChecked$, FalseY�$��Filter Records��$��Center=�$��Center=�� - � AllowsTransparency- . $��Slide=����� $ 	chkFilter��$ 	IsChecked�. � PlacementTarget. �� $ 	chkFilter��.���� $	��180��$	��146������ / �� EndPoint$/ 0.5,1&�0 �� 
StartPoint$0 0.5,0&�.��� 1 � Color$1 Black��2 � Offset$2 1Q�� $1 	#FFF1EFC9��$2 0.206Q�� $1 	#FF7D8575��$2 0.041Q�� $1 	#FFC4E2EB��$2 0.254Q�� $1 	#FFA5CF91��$2 0.911Q�z��� -.   theTransform1��.+��� $ Vertical=�.+��� $��Transaction Date Range��$��12(�$��Italic$�$	��180��$
��Left=��� $ 
Horizontal=�.+��� $��From:��$��12(�$��Italic$�$��45��$��Right=�  -/   $ xctk_dtpFromDate��#��[ $��20��$	��130���� $ 
Horizontal=�.+��� $	��To:��$��Italic$�$��45��$��Right=�  -0   $ xctk_dtpToDate��#��[ $��20��$	��130���� -1   $ 	btnFilter��$��	Go Filter��$��20��$��60��$��12(�$��Verdana)�#��[ 3 �� Left$3 60��
4 �� Top$	4 116���� $��30��$y�Hidden=��� $ 
Horizontal=�$��Right=�$��Right=�.+��� $��10��$y�Hidden=��� $
��Edit��#��[ $��80��$��22���� $��10��$y�Hidden=��� $��Delete��#��[ $��80��$��22���� $��10��$y�Hidden=��� -2   $ 	chkPrint2��$, FalseY�$��Print Memo/List��$��Center=�$��Center=�� - . $��Slide=����� $ 	chkPrint2��$ 	IsChecked�. �� $ 	chkPrint2��.���� $	��134��$	��146������ $/ 0.5,1&�$0 0.5,0&�.��� $1 Black��$2 1Q�� $1 	#FFF1EFC9��$2 0.206Q�� $1 	#FF7D8575��$2 0.041Q�� $1 	#FFC4E2EB��$2 0.254Q�� $1 	#FFA5CF91��$2 0.911Q�z��� -3   theTransform2��.+��� $ Vertical=�$	��134��.+��� $��Print Options��$��12(�$��Italic$�$
��Bold"�
���**��$
��Left=��� $ Vertical=�$��10,0,0,0q�.+�	� -4   5 	�Name$5 rb3SelectedMemo��$��Selected Memo��$
, TrueY��� $y�Hidden=�	� -5   $5 rb3TableRecoreds��$��Table Records���� �� $ Vertical=�$��10,2,0,0q�.+�	� -6   $5 rb3QuickPrint��$��Quick Print���� $y�Hidden=�	� -7   $5 rb3PrintPreview��$��Print Preview��$
, TrueY��� -8   $ 	btnPrint3��$��Go Print��$��20��$��60��$��12(�$��Verdana)�#��[ $3 34��$	4 116���� $��20��$y�Hidden=�