   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      -    'ASITHmsWpf.Accounting.frmEntryAccMgt103   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4UC%clr-namespace:ASITHmsWpf.UserControls     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5        
  � Tag$  Advice Letters To Bank��5	   8      IOUserControl#��[  6U   $��14(�6|   .��5
      � .+�5   
   �� .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��Dated From :��6   $��90��63   $��Right=�6>   &   Xceed.Wpf.Toolkit.DateTimePicker  5      -   5          Name$ xctk_dtpFrom��5   *     StdDateTimePickerStyle01#��[ 6@   $
��Bold"�6r   $��22��6�   $	��120��6�   �� 5      $
��To :��6   $��30��6+   $��Right=�66     5      -   5        $ 
xctk_dtpTo��5   *   #��[ 6>   $
��Bold"�6p   $��22��6�   $	��120��6�   5      �� 5      -   5         ��Name$ lblChqBankTitle��5      $��_Bank A/C :��61   $��Right=�6G   $��90��6j   $��14(�6u   $
��Bold"�6�    �� Target 6�   ��  �� ElementName$ 
cmbChqBank���� 5      -   5         ��Name$ 
cmbChqBank��5      
�������$	��500��6B    �� 
IsReadOnly .  �� SelectedIndex$ 0��6`   $��12(�6r   $
��Bold"�6�   �� 5      -   5        	 ��Name$	 btnShowVoucher��5        StdBtnStyle01#��[ 61   $��Show Vouchers��6X   $	��100��6p   $��10,0,0,0q�6|   6�   �� 5      -   5        $	 btnPrintLetter��5      #��[ 61   $��Print Letter��6X   $	��100��6o   $��10,0,0,0q�6{   6�   5      �� 5      -   5        
 ��Name$
 stkpBookStatus��5      $��0,5,0,0q�61   .+�5      &   System.Windows.Controls.DataGrid  -	   5          Name$ 
dgVouList1��5         CanUserResizeRows .  
������    
IsReadOnly .  $��0q�6u   $��0,0,0,0q�6�   $��0��6�   $	��Top=�5      $	��450��67   $
��Auto=�6G      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  $��Hidden=�5         SelectionUnit$
 Cell=�6S      SelectionMode$ Single=�6h      Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 35 61      Header$	 Sl#��6<      
IsReadOnly .    CellTemplate 5      �� 5   "   .R�5    &   � $��Right=�6+   $	��Top=�6G   $��30��6_   .+�5!   *   �� $��Right=�64   $	��Top=�6P   ��6i   �� *slnum+ �� StringFormat$ 
{0: ##0) }��5"   '   5#   #   5%      0  *System.Windows.Controls.DataGridTextColumn  5&      $ 80 6-   $ 
Cheque No.��68      CanUserSort .  1  +System.Windows.Controls.DataGridBoundColumn   Binding 6L   ��  �� Path$ chqref�6l     5'      $ 20 61      
Visibility$ Visible=�6=    5(      �� 5)   "   .R�5*   &   ��  d� 	IsChecked 6/   �� $ Mark1� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5+   #   5-        5.      $	 140 6-   $ Voucher No.��69     +� IsFocusScope  .   .  .  !   CanUserReorder! .  "   CanUserResize" .   6N   �� $ vounum1�6�     5/      $	 100 6-   $ Cheque Date��69     .   .   . ! .  " .   6N   �� $ voudat�$ {0: dd-MMM-yyyy}��6�     50      $	 120 62   $ Cheque Amount��6>    51      �� 52   "   .R�53   &   �� # �� TextAlignment$# Right=�6z   ��61   �� $ trnam�$  {0: #,##0.00;(#,##0.00); }��6z   54   #   56        57      $	 400 6-   $ Amount in word��69     .   .  ! .  " .   6Q   �� $ aminw�6�   59      5:      5;      5<      5=      