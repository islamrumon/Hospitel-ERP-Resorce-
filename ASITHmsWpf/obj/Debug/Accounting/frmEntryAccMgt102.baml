   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      -    'ASITHmsWpf.Accounting.frmEntryAccMgt102   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4UC%clr-namespace:ASITHmsWpf.UserControls     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5        
  � Tag$  Cheque Book Register��5	         IOUserControl#��[  6)   $��14(�6P   .��5
      � .+�5   
   ��  �� Orientation$ Vertical=�6   $��	10,10,0,0q�6,   .+�5      �� $ 
Horizontal=�6   $	��Top=�62   $
��Left=�6J   .+�5      �� -   5         ��Name$ lblChqBankTitle��5      $��_Bank Name:��61   $��Right=�6G   $	��120��6j   $��14(�6v   $
��Bold"�6�    �� Target 6�   ��  �� ElementName$ 
cmbChqBank���� 5      -   5         ��Name$ 
cmbChqBank��5      
�������$	��500��6B    �� 
IsReadOnly .  �� SelectedIndex$ 0��6`   $��14(�6r   $
��Bold"�6�   �� 5      -   5         ��Name$ btnBookNext��5        StdBtnStyle01#��[ 6.   $
��Next��6U   $��80��6d   $��10,0,0,0q�6o   $y�Hidden=�6�   �� 5      -   5        $ btnExistBook��5      #��[ 6/   $��Show Existing Books��6V   $	��120��6t   $��72,0,0,0q�6�   6�   �� 5      -   5        $ 
btnRegBook��5      #��[ 6-   $��Add New Book��6T   $	��120��6k   $��15,0,0,0q�6w   6�   5      �� 5      -   5        	 ��Name$	 stkpBookList��5      $ 
Horizontal=�6/   $��0,10,0,0q�6H   $y�Visible=�6Z   $��32��6o   .+�5      �� -   5        $ lblExistChqBookTitle��5      $��Existing _Books:��66   $��Right=�6Q   $	��120��6t   $��14(�6�   $
��Bold"�6�    6�   �� $ cmbExistChqBook���� 5      -	   5        $ cmbExistChqBook��5      
�������$	��760��6G    . $ 0��6e   $��14(�6w   $
��Bold"�6�   6�   �� 5      -
   5        $ btnShowLeafs��5      #��[ 6/   $��Show Status��6V   $	��120��6l   $��13,0,0,0q�6x   6�   5      �� 5      -   5        $	 stkpNewBookReg��5      $ Vertical=�61   $��0,0,0,0q�6H   $y�Visible=�6Y   .+�5      �� $ 
Horizontal=�6   .+�5      �� -   5        
 ��Name$
 chkDateBlocked��5      $��Registration _Date : ��67   |�.  $��Right=�6i   $��Center=�6�   $	��120��6�   $��	120,0,0,0q�6�   &   Xceed.Wpf.Toolkit.DateTimePicker  5      -   5          Name$ xctk_dtpRegDat��5   *     StdDateTimePickerStyle01#��[ 6B   $
��Bold"�6t   $��24��6�   $	��130��6�   {�6�   �� $ chkDateBlocked�� �� Path$ 	IsChecked��� 5      -   5        $ lblLeafStartTitle��5      $��Leaf Start No.:��67   $��Right=�6Q   $	��100��6t   $��26��6�    6�   �� $ txtLeafStart��(  "Xceed.Wpf.Toolkit.WatermarkTextBox  5      -   5          Name$ txtLeafStart��5   ,    �� TextAlignment$ Right=�6B   $��Center=�6X   )  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .    AutoSelectBehavior$ OnFocus=�6�    
 wtxtBx#��[ 5   &      	Watermark$  ��6F   $�� ��6S   $
��Left=�6[   $��24��6}   $	��150��6�   �� 5!      -   5        $ lblLeafQtyTitle��5!      $��Leaf Quantity:��65   $��Right=�6N   $	��100��6q   $��26��6}    6�   �� $ 
txtLeafQty���� 5"      -   5        $ 
cmbLeafQty��5"      $��50��63   .@�5#      �� $��10��6'   $  10��64   �� 5$      $��20��6'   $  20��64   �� 5%      $��25��6'   $  25��64   �� 5&      $��50��6'   $  50��64    �� 
IsSelected . 6=   �� 5'      $	��100��6'   $	  100��65   5(      �� 5)      -   5        $ 
btnGenLeaf��5)      #��[ 61   $��Show New Leaves��6X   $	��120��6r   $��13,0,0,0q�6~   6�   �� 5*      -   5        $ btnUpdateNewLeaf��5*      #��[ 67   $��Save New Book��6^   $	��120��6v   $��13,0,0,0q�6�   6�   5+      �� 5,      $ 
Horizontal=�6   $��0,10,0,0q�66   .+�5-      �� -   5        $
 
chkChqRmrk��5-      $��Rrmarks (if any):��63   $��Center=�6O   $	��120��6j   |�.  $��	120,0,0,0q�6�     5.      -   5        $ 
txtChqRmrk��5.   ,   $
 Left=�6A   $��Center=�6V    . $ OnFocus=�5/   q   $��26��6�   $��5,0,0,0q�50   4   #��[ 6F   $  ��6f   $�� ��6s   $
��Left=�6{   $	��530��6�   |�5/   4   �� $ 
chkChqRmrk��$ 	IsChecked�50   �   51      52      �� 53      -   5        $	 stkpBookStatus��53      $��0,5,0,0q�61   .+�54      &   System.Windows.Controls.DataGrid  -   5          Name$ 
dgChqLeaf1��54         CanUserResizeRows .  
������    
IsReadOnly .  $��0q�6u   $��0,0,0,0q�6�   $��0��6�   $	��Top=�55      $	��450��67   $
��Auto=�6G      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  $��Hidden=�56         SelectionUnit$
 Cell=�6S      SelectionMode$ Single=�6h      AlternatingRowBackground �	#FFE4F2E1   Columns 57      4  .System.Windows.Controls.DataGridTemplateColumn  58      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 35 61      Header$	 Sl#��6<      
IsReadOnly .     CellTemplate  59      �� 5:   "   .R�5;   &   � $��Right=�6+   $	��Top=�6G   $��30��6_   .+�5<   *   �� $��Right=�64   $	��Top=�6P   ��6i   �� *slnum+! �� StringFormat$! 
{0: ##0) }��5=   '   5>   #   5@      0  *System.Windows.Controls.DataGridTextColumn  5A      $ 80 6-   $ 
Cheque No.��68   "   CanUserSort" .   . 1	  +System.Windows.Controls.DataGridBoundColumn# 	  Binding# 6L   �� $ cheqnum�6�     5B      $	 200 6-   $ Voucher No.��69   $ +� IsFocusScope$ .   . " .  %   CanUserReorder% .  &   CanUserResize& .  # 6N   �� $ vounum1�6�     5C      $	 100 6-   $ Cheque Date��69   $ .  " .   . % .  & .  # 6N   �� $ cheqdat1�6�     5D      $	 120 62   $ Cheque Amount��6>    .   5E      �� 5F   "   .R�5G   &     -   5        $ Right=�5G   <    . $��0q�6�   $	��110��6�   $ OnFocus=�5H   0   $��5,0,0,0q�6M   #��[ 6_   $ Amount��6   ' �� 
IsReadOnly' . ��5I   <   �� $ cheqam�( �� Mode$( TwoWay=�) �� UpdateSourceTrigger$) PropertyChanged=�$ ! {0: #,##0.00;(#,##0.00); }��5J   #   5L        5M      $	 100 6-   $ Delivery Date��69   $ .  " .  % .  & .  # 6P   �� $ 	delivdat1�6�     5N      $ 45 6-   $ Status��68   $ .  " .  % .  & .  # 6H   �� $ 
cheqstatus�6�     5O      $	 300 6-   $ Remarks��69   $ .  " .  % .  & .  # 6J   �� $ 
statusnote�6�   5Q      5R      5S      5T      5U      