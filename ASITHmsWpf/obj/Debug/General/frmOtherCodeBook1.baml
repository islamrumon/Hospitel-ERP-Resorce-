   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      'clr-namespace:ASITHmsWpf
ASITHmsWpf  5      *    $ASITHmsWpf.General.frmOtherCodeBook1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      $localclr-namespace:ASITHmsWpf  5      -   5        
  � Tag$:  4Others Code (Medicine) Information Input/Edit Screen��5	          IOUserControl#��[  6x   .��5
      � .+�5   
   �� .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0��62   .+�5      �� $��_Main Category :��6   $	��110��63   $��Right=�6j    �� Target 6?   ��  �� ElementName$ cmbMainCat1��6j   �� 5      -   5         ��Name$ cmbMainCat1��5      $	��430��60   6n   �� 5      $��Su_b Category :��6   $��95��62   $��Right=�6=    6`   �� $ 
cmbSubCat1���� 5      -   5        $ 
cmbSubCat1��5      $	��430��6/   6=   �� 5      -   5         ��Name$ btnShow��5      $
��Show��6*   $��75��69   $	��5,0q�6E     StdBtnStyle01#��[ 6h   5      �� 5      -   5         ��Name$ stkedit��5      $��1��6*   $��0,5,0,0q�68   .+�5      �� $ 
Horizontal=�6   .+�5      �� -   5         ��Name$ 
lblSirCode��5      $  XXXXXXXXXXXX��60   $��Item Code :��6C   $	��110��6Y   $��Right=�6e   �� 5      $ 
Horizontal=�6!   .+�5      (  "Xceed.Wpf.Toolkit.WatermarkTextBox  -   5          Name$ txtSirCode1��5   0   {�.  $��Center=�6W   $��22��6{   	 �� 	MaxLength$	 2��6�   $��25��6�   )  #Xceed.Wpf.Toolkit.AutoSelectTextBox
   AutoMoveFocus
 .    	Watermark$ xx��6�      AutoSelectBehavior$ OnFocus=�6�   6�   �� 5      $��-��6    $��10(�6,     5      -   5        $ txtSirCode2��5   0   {�.  $��Center=�6W   $��22��6{   $	 2��6�   $��25��6�   
 . $ xx��6�   $ OnFocus=�6�   6�   �� 5      $��-��6    $��10(�6,     5      -	   5        $ txtSirCode3��5   0   {�.  $��Center=�6W   $��22��6{   $	 3��6�   $��30��6�   
 . $	 xxx��6�   $ OnFocus=�6�   6�   �� 5      $��-��6    $��10(�6,     5      -
   5        $ txtSirCode4��5   0   {�.  $��Center=�6W   $��22��6{   $	 2��6�   $��25��6�   
 . $ xx��6�   $ OnFocus=�6�   6�   �� 5      $��-��6    $��10(�6,     5      -   5        $ txtSirCode5��5   0   $��Center=�6E   $��22��6i   $	 3��6u   $��30��6�   
 . $	 xxx��6�   $ OnFocus=�6�   6�   5       �� 5!      $��_Title Desc :��6   $��75��65   $��Right=�6@    6c   �� $ wtTxtsirdesc��  5"      -   5        $ wtTxtsirdesc��5"   ^    
 wtxtBx#��[ 6,   $��5,0,0,0q�6L   $��22��6u    �� TextWrapping$
 Wrap=�5#   ,   $	��400��6A   $ OnFocus=�6M   �� 5$      $��Unit :��6   $��50��6-   $��Right=�68     5%      -   5        $ wtTxtsirunit��5%   ^   #��[ 6,   $��5,0,0,0q�6L   $��22��5&   ,   $
 Wrap=�68   $��40��6L   $ OnFocus=�6W   �� 5'      $��Unit C_ost :��6   $��65��63    6?   �� $ 
wtTxtcostP��  5(      -   5        $ 
wtTxtcostP��5(   L   #��[ 6,   $��22��5)   ,   $��Right=�69   $��75��6\   
 . $ OnFocus=�6}   �� 5*      $��Sales _Rate:��6   $��65��63   $��Right=�6>    6a   �� $ wtTxtsalesP��  5+      -   5        $ wtTxtsalesP��5+   p   #��[ 6,   $��Right=�6M   $��22��5,   ,   $��75��68   
 . $ OnFocus=�6Y   5-      �� 5.      $ 
Horizontal=�6   .+�5/      �� $��Mfg. I_D(Bar Code) :��6   $	��110��6;    6G   �� $ wtTxtMfgCode��  50      -   5        $ wtTxtMfgCode��50   L   #��[ 6,   $��22��6b   $
 Wrap=�6n   $	��165��6�   $ OnFocus=�6�   �� 51      -   5        $ 	lblMfName��51   4   $��Mfg Company :��6   $	��100��6G   $��Right=�6S   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  52      -   5          Name$ AtxtMfgComp��52   -      
SearchType$ C��6B   $
��Left=�6Q   $��22��6s   $	��400��6   �� 54      $��Usage :��6   $��50��6.   $��Right=�69     55      -   5        $ 
wtTxtUsage��55   ^   #��[ 6,   $��5,0,0,0q�6L   $��22��6r   $	��320��6~   $ OnFocus=�6�   57      �� 58      $ 
Horizontal=�6   $��0,5,0,0q�66   .+�59      �� $��Full Name of Item :��6   $	��110��6:   $��Right=�6F     5:      -   5        $ wtTxtFullName��5:   L   #��[ 6,   $��22��6c   $
 Wrap=�6o   $	��500��6�   $ OnFocus=�6�   �� 5;      $��Gen. Ingrediants :��6   $	��110��69   $��Right=�6E     5<      -   5        $ wtTxtGenericItems��5<   L   #��[ 6,   $��22��6g   $
 Wrap=�6s   $	��425��6�   $ OnFocus=�6�   5=      �� 5>      $ 
Horizontal=�6   .+�5?      �� $��Compos. Details :��6   $	��110��68   $��Right=�6D     5@      -   5        $ wtTxtChStruc��5@   L   #��[ 6,   $��40��6b   $
 Wrap=�6n   $
��1035��6�   $		 250��6�    �� AcceptsReturn . $ OnFocus=�6�   5A      �� 5B      $ 
Horizontal=�6   .+�5C      �� $��W.Sale Pack Size :��6   $	��110��69   $��Right=�6E     5D      -   5        $ wtTxtWPackSize��5D   L   #��[ 6,   $��22��6d   $
 Wrap=�6p   $	��165��6�   $ OnFocus=�6�   �� 5E      $��Retail Pack Size :��6   $	��100��69   $��Right=�6E     5F      -   5        $ wtTxtRPackSize��5F   L   #��[ 6,   $��22��6d   $
 Wrap=�6p   $	��235��6�   $ OnFocus=�6�   �� 5G      $��Remarks (if any) :��6   $	��110��69   $��Right=�6E     5H      -   5        $ wtTxtRemakrs��5H   L   #��[ 6,   $��22��6b   $
 Wrap=�6n   $	��425��6�   $ OnFocus=�6�   5I      5J      � 5K      -   5         �Name$ 
grListView��5K      $��2��6'   .+�5L      �� $	��Top=�6   .+�5M      �� $ 
Horizontal=�6!   
�������.+�5N      �� -   5        $ lblItemSearch��5N       $��_Search Item :��67   $	��110��6P   $��Right=�6\    6   �� $ AtxtItemCode��  5O      -   5        $ AtxtItemCode��5O   1   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox   	LostFocus$ AtxtItemCode_LostFocus��6�   $ C��6G   $
��Left=�6V   $��22��6x   $	��430��6�   6�   �� 5P      -   5        $ lblUnit1��5P       $
��Unit��62   $��50��6A   �� 5Q      -   5        $ 
btnEditItm��5Q   !   $��	Edit Item��65   $��24��6I   $��	130,0,0,0q�6U   $��75��6h   #��[ 6s   �� 5R      -   5        $ btnAddNewItm��5R   !   $��Add New��67   $��24��6I   $��50,0,0,0q�6U   $��75��6g   #��[ 6r   6�   �� 5S      -    5        $ 	btnUpdate��5S   !   $��_Update��64   $��24��6F   $��50,0,0,0q�6R   $��75��6d   #��[ 6o   6�   5U      &   System.Windows.Controls.DataGrid  5V      -!   5          Name$ dgvItem1��5V       
������ $��0q�6K   $��0,0,0,0q�6_   $��0��6p   $	��Top=�5W      $	��300��67      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserResizeRows .     	RowHeight$ 20Q�6�      
IsReadOnly .  $
��Auto=�5X   2   $��Hidden=�6b      SelectionMode$ Single=�6�      FrozenColumnCount$ 2��6�      Columns 5Y      0  *System.Windows.Controls.DataGridTextColumn  5Z      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 40 61      Header$	 Sl#��6<    +� IsFocusScope .      CanUserSort  .  !   
IsReadOnly! . "   CanUserReorder" .  #   CanUserResize# .  1	  +System.Windows.Controls.DataGridBoundColumn$ 	  Binding$ 6I   �� % �� Path$% slnum�6�     5[      $	 100 61   $ 	Item Code��6=    .    .  ! . " .  # .  $ 6P   �� $% sircode�6�     5\      $	 320 61   $ Title Description��6=    .    .  ! . " .  # .  $ 6X   �� $% sirdesc�6�     5]      $ 50 61   $
 Unit��6<    .    .  ! . " .  # .  $ 6J   �� $% sirunit�6�     5^      $	 250 61   $ Manufacturer��6=    .    .  ! . " .  # .  $ 6S   �� $% 	mfgcomnam�6�   4
  .System.Windows.Controls.DataGridTemplateColumn
  5_      $	 100 65   $ 	Unit Cost��6A   & 
  CellTemplate& 5`   "   �� 5a   &   .R�5b   *   � $��Right=�6/   $	��Top=�6K   $��90��6c   .+�5c   .   �� $��Right=�68   $	��Top=�6T   ��6m   �� *	costprice+' �� StringFormat$"' {0: #,##0.00;-0: #,##0.00; }��5d   +   5e   '   5g      
  5h      $	 100 65   $ 
Sales Rate��6A   & 5i   "   �� 5j   &   .R�5k   *   � $��Right=�6/   $	��Top=�6K   $��90��6c   .+�5l   .   �� $��Right=�68   $	��Top=�6T   ��6m   �� *	saleprice+$"' {0: #,##0.00;-0: #,##0.00; }��5m   +   5n   '   5p        5s      $	 150 61   $ Gen. Ingrediants��6=    .    .  ! . " .  # .  $ 6W   �� $% genrnam�6�   5u      5�      5�      5�      5�      5�      