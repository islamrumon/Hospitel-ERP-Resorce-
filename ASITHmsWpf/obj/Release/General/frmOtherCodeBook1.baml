   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  'clr-namespace:ASITHmsWpf
ASITHmsWpf  *    $ASITHmsWpf.General.frmOtherCodeBook1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  $localclr-namespace:ASITHmsWpf  -   
  � Tag$4  .Others Code Book Information Input/Edit Screen��   IOUserControl#��[  .��� .+��� -    ��Name$ 	tabPanel1�� �� SelectedItem$ 4��$��Stretch=�$��Stretch=� �� TabStripPlacement$	 Top=�$��20,20,20,60q�.@��� $��Medicine Item��.��� ����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FFE4F2E1�� � Offset$ 0.491Q�� $ 	#FF69B28B��$ 0.957Q� � RowDefinitions �� $��25 ��� $	��160 ��� .+��� 	 �� Orientation$	 
Horizontal=�$��0��.+��� $��_Main Category :��$	��110��$��Right=�
 �� Target
 ��  �� ElementName$ cmbMainCat1���� -    ��Name$ cmbMainCat1��$	��430���� $��Su_b Category :��$��95��$��Right=�
 �� $ 
cmbSubCat1���� -   $ 
cmbSubCat1��$	��430���� -    ��Name$ btnShow��$
��Show��$��75��$	��5,0q�  StdBtnStyle01#��[ �� -    ��Name$ stkedit��$��1��$��0,5,0,0q�.+��� $	 
Horizontal=�.+��� -    ��Name$ 
lblSirCode��$  XXXXXXXXXXXX��$��Item Code :��$	��110��$��Right=��� $	 
Horizontal=�.+�(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ txtSirCode1��{�.  $��Center=�$��22�� �� 	MaxLength$ 2��$��25��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .    	Watermark$ xx��   AutoSelectBehavior$ OnFocus=��� $��-��$��10(�  -	   $ txtSirCode2��{�.  $��Center=�$��22��$ 2��$��25�� . $ xx��$ OnFocus=��� $��-��$��10(�  -
   $ txtSirCode3��{�.  $��Center=�$��22��$ 3��$��30�� . $	 xxx��$ OnFocus=��� $��-��$��10(�  -   $ txtSirCode4��{�.  $��Center=�$��22��$ 2��$��25�� . $ xx��$ OnFocus=��� $��-��$��10(�  -   $ txtSirCode5��$��Center=�$��22��$ 3��$��30�� . $	 xxx��$ OnFocus=��� $��_Title Desc :��$��75��$��Right=�
 �� $ wtTxtsirdesc��  -   $ wtTxtsirdesc�� 
 wtxtBx#��[ $��5,0,0,0q�$��22�� �� TextWrapping$
 Wrap=�$	��400��$ OnFocus=��� $��Unit :��$��50��$��Right=�  -   $ wtTxtsirunit��#��[ $��5,0,0,0q�$��22��$
 Wrap=�$��40��$ OnFocus=��� $��Unit C_ost :��$��65��
 �� $ 
wtTxtcostP��  -   $ 
wtTxtcostP��#��[ $��22��$��Right=�$��75�� . $ OnFocus=��� $��Sales _Rate:��$��65��$��Right=�
 �� $ wtTxtsalesP��  -   $ wtTxtsalesP��#��[ $��Right=�$��22��$��75�� . $ OnFocus=��� $	 
Horizontal=�.+��� $��Mfg. I_D(Bar Code) :��$	��110��
 �� $ wtTxtMfgCode��  -   $ wtTxtMfgCode��#��[ $��22��$
 Wrap=�$	��165��$ OnFocus=��� -   $ 	lblMfName��$��Mfg Company :��$	��100��$��Right=�1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -     Name$ AtxtMfgComp��   
SearchType$ C��$
��Left=�$��22��$	��400���� $��Usage :��$��50��$��Right=�  -   $ 
wtTxtUsage��#��[ $��5,0,0,0q�$��22��$	��320��$ OnFocus=��� $	 
Horizontal=�$��0,5,0,0q�.+��� $��Full Name of Item :��$	��110��$��Right=�  -   $ wtTxtFullName��#��[ $��22��$
 Wrap=�$	��500��$ OnFocus=��� $��Gen. Ingrediants :��$	��110��$��Right=�  -   $ wtTxtGenericItems��#��[ $��22��$
 Wrap=�$	��425��$ OnFocus=��� $	 
Horizontal=�.+��� $��Compos. Details :��$	��110��$��Right=�  -   $ wtTxtChStruc��#��[ $��40��$
 Wrap=�$
��1035��$	 250�� �� AcceptsReturn . $ OnFocus=��� $	 
Horizontal=�.+��� $��W.Sale Pack Size :��$	��110��$��Right=�  -   $ wtTxtWPackSize��#��[ $��22��$
 Wrap=�$	��165��$ OnFocus=��� $��Retail Pack Size :��$	��100��$��Right=�  -   $ wtTxtRPackSize��#��[ $��22��$
 Wrap=�$	��235��$ OnFocus=��� $��Remarks (if any) :��$	��110��$��Right=�  -   $ wtTxtRemakrs��#��[ $��22��$
 Wrap=�$	��425��$ OnFocus=�� -    �Name$ 
grListView��$��2��.+��� $	��Top=�.+��� $	 
Horizontal=�
�������.+��� -   $ lblItemSearch��$��_Search Item :��$	��110��$��Right=�
 �� $ AtxtItemCode��  -   $ AtxtItemCode��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox   	LostFocus$ AtxtItemCode_LostFocus��$ C��$
��Left=�$��22��$	��430���� -   $ lblUnit1��$
��Unit��$��50���� -   $ 
btnEditItm��$��	Edit Item��$��24��$��	130,0,0,0q�$��75��#��[ �� -    $ btnAddNewItm��$��Add New��$��24��$��50,0,0,0q�$��75��#��[ �� -!   $ 	btnUpdate��$��_Update��$��24��$��50,0,0,0q�$��75��#��[ &   System.Windows.Controls.DataGrid  -"     Name$ dgvItem1��
������ $��0q�$��0,0,0,0q�$��0��$	��Top=�$	��300��   AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserResizeRows .      	RowHeight$  20Q�!   
IsReadOnly! .  $
��Auto=�$��Hidden=�"   SelectionMode$" Single=�#   FrozenColumnCount$# 2��$   Columns$ 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn%   Width5  /System.Windows.Controls.DataGridLengthConverter$% 40 &   Header$	& Sl#��' +� IsFocusScope' .  (   CanUserSort( .  )   
IsReadOnly) . *   CanUserReorder* .  +   CanUserResize+ .  1	  +System.Windows.Controls.DataGridBoundColumn, 	  Binding, �� - �� Path$- slnum�  $	% 100 $& 	Item Code��' .  ( .  ) . * .  + .  , �� $- sircode�  $	% 320 $& Title Description��' .  ( .  ) . * .  + .  , �� $- sirdesc�  $% 50 $
& Unit��' .  ( .  ) . * .  + .  , �� $- sirunit�  $	% 250 $& Manufacturer��' .  ( .  ) . * .  + .  , �� $- 	mfgcomnam�4
  .System.Windows.Controls.DataGridTemplateColumn
  $	% 100 $& 	Unit Cost��. 
  CellTemplate. �� .R�� $��Right=�$	��Top=�$��90��.+��� $��Right=�$	��Top=����� *	costprice+/ �� StringFormat$"/ {0: #,##0.00;-0: #,##0.00; }��
  $	% 100 $& 
Sales Rate��. �� .R�� $��Right=�$	��Top=�$��90��.+��� $��Right=�$	��Top=����� *	saleprice+$"/ {0: #,##0.00;-0: #,##0.00; }��  $	% 150 $& Gen. Ingrediants��' .  ( .  ) . * .  + .  , �� $- genrnam��� -#   0 ��Name$0 
tabSirCode��$��Item Code Book��.��� .+��� $
��Left=�$��10,10,10,10q�.+��� -$   $ btnAddSirCode��$��Add Item��$	��120��$��25��|�.  