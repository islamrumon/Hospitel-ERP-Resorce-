   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      9    3ASITHmsWpf.Commercial.ParkTicket.frmEntryParkPOS102   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4UC%clr-namespace:ASITHmsWpf.UserControls     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5        
  � Tag$%  PARK TICKET/COUPON SALES RETURN��5	   8   $��14(�6^   .��5
      � .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� .+�5      �� $"��Ticket Cancel (Sales Return)��6   $
��1190��6@    �� TextAlignment$ Center=�6M   $��Center=�6d   $��Center=�6   
�������6�   �� 5      $ Vertical=�6   $��	05 10 0 0q�64   .+�5      �� $ 
Horizontal=�6!   $��5,0,0,0q�6:   .+�5      �� $��Date :��6    $��50��61   $��Right=�6<   &   Xceed.Wpf.Toolkit.DateTimePicker  5      -   5          Name$ xctk_dtpInvDat��5   .      StdDateTimePickerStyle01#��[  6F   $
��Bold"�6x   $��24��6�   $	��120��6�   �� 5      $��Location : ��6$   $��70��67   $ Right=�6B   $��24��6X   �� 5      -   5         ��Name$ 
cmbSectCod��5   #   $	��280��67   $��24��6C   $
��Bold"�6P    �� SelectedIndex$ 0��6b   
�������$��0q�6�   
������ {�. 5   E   5      �� 5      -   5         ��Name$ stkpOptItem��5   !   $ 
Horizontal=�66   .+�5      �� -   5         ��Name$ lblItemIdTitle��5       $��Ticket :��68   $��Right=�6K   $��60��6n   *   $ASITHmsWpf.UserControls.AutoComplete  5      -   5          Name$ autoItemSearch��5   *   *   $ASITHmsWpf.UserControls.AutoComplete	   PatternChanged$#	 autoItemSearch_PatternChanged��6�   
   MouseDoubleClick$%
 autoItemSearch_MouseDoubleClick��5   m   $	��230��5   B    �� SelectedValuePath$ sircode��6N    �� DisplayMemberPath$ sirdesc��6j   $��12(�6�    % !AutoCompleteComboBoxWithoutButton#��[ 5         Delay$	 500��6U   $��24��6a    � ContextMenu 5      �� 5   "   5      �� 5      -   5         ��Name$ btnGenerate��5   !     StdBtnStyle01#��[ 66   $��_Show��6]   $
��Bold"�6m   $��80��6   $��24��6�   $��10,0,0,0q�6�   �� 5       -   5        $ 	btnUpdate��5    !   #��[ 64   $��_Update��6[   $
��Bold"�6m   $��80��6   $��24��6�   $��10,0,0,0q�6�   5!      �� 5"      $ 
Horizontal=�6!   .+�5#      �� -	   5        $ btnAllChecked��5#   !   $��Select _All��68   $��80��6N   $��24��6Y   $��0,0,0,0q�6�   �� 5$      -
   5        $ btnAllUnchecked��5$   !   $��	Clear All��6:   $��80��6N   $��24��6Y   $��10,0,0,0q�6�   �� 5%      -   5        $ lblFrom��5%       $��From :��61   $��60��6B   $��Right=�6M   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5&      -   5          Name$ 	txtFromNo��5&   1   $	��100��6D   $��24��6P   �� 5'      -   5        $ lblTo��5'       $
��To :��6/   $��30��6>     5(      -   5        $ txtToNo��5(   0   $	��100��6A   $��24��6M   �� 5)      -   5        $ btnSet��5)   !   #��[ 61   $
��Bold"�6X   $	��Set��6j   $��80��6x   $��24��6�   $��10,0,0,0q�6�   �� 5*      -   5        $ 
btnRefresh��5*   !   #��[ 65   $
��Bold"�6\   $��Refresh��6n   $��80��6�   $��24��6�   $��10,0,0,0q�6�   5+      5,      �� 5.      -   5        $ stkpDataGrid��5.      $ 
Horizontal=�63   .+�5/      &   System.Windows.Controls.DataGrid  -   5          Name$ dgOverall01��5/         CanUserResizeRows .  
������    
IsReadOnly .  $��0q�6z   $��0,05,0,0q�6�   $	��300��6�   $	��Top=�50      $
��Left=�67   $	��400��6R   $
��Auto=�6_      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  $��Hidden=�51         SelectionUnit$
 Cell=�6S      SelectionMode$ Single=�6h      GridLinesVisibility$	 All=�6      Columns 52      4  .System.Windows.Controls.DataGridTemplateColumn  53      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 40 65      Header 54   "   �� 55   &   $
��Mark��60   $��12(�6<      CellTemplate 57   "   �� 58   &   .R�59   *   � .+�5;   .   ��  ��Name$ chkTicketcnt��67   $��60��6�   $��12(�6�   $��Center=�6�   $��10,0,0,0q�6�    d� 	IsChecked 6M   ��   �� Path$  	tokencnt1�! �� Mode$! TwoWay=�" �� UpdateSourceTrigger$" PropertyChanged=�6�   5<   +   5=   '   5?        5@      $ 80 65    5A   "   �� 5B   &   $��
Ticket No.��60   $��80��6B   $��12(�6M    5D   "   �� 5E   &   .R�5F   .   �� 
������ # �� 
IsReadOnly# . $��0q�6   ��66   �� $  maxtnum�6   5G   '   5I        5J      $ 55 65    5K   "   �� 5L   &   $	��Qty��60   $��55��6;   $��12(�6F    5N   "   �� 5O   &   .R�5P   *   �� $ Right=�6n   $��50��6�   ��64   �� $  perscnt1�$ �� StringFormat$$ 
{0: #,##0}��6�   5Q   '   5S        5T      $	 100 65    5U   "   �� 5V   &   $��Amount��60   $��80��6>   $ Right=�6I   $��12(�6_    5X   "   �� 5Y   &   .R�5Z   *   � .+�5[   .   �� $��90��6s   $ Right=�6~   ��68   �� $  tnetam�$$ {0: #,##0.00}��6~   5\   +   5]   '   5_      5a        5b      -   5        $ dgOverall02��5b       .  
������  .  $��0q�6z   $��	10,05,0,0q�6�   $	��300��6�   $	��Top=�5c      $
��Left=�67   $	��400��6R   $
��Auto=�6_    .   .   .  $��Hidden=�5d      $
 Cell=�6S   $ Single=�6h   $	 All=�6    5e        5f      $ 40 65    5g   "   �� 5h   &   $
��Mark��60   $��12(�6<    5j   "   �� 5k   &   .R�5l   *   � .+�5m   .   �� $ chkTicketcnt��67   $��45��6�   $��12(�6�   $��Center=�6�   $��10,0,0,0q�6�    6M   �� $  	tokencnt1�$! TwoWay=�$" PropertyChanged=�6�   5n   +   5o   '   5q        5r      $ 80 65    5s   "   �� 5t   &   $��
Ticket No.��60   $��70��6B   $��12(�6M    5v   "   �� 5w   &   .R�5x   *   � .+�5y   .   �� ��68   �� $  maxtnum�5z   +   5{   '   5}        5~      $ 55 65    5   "   �� 5�   &   $	��Qty��60   $��55��6;   $��12(�6F    5�   "   �� 5�   &   .R�5�   *   � .+�5�   .   �� $ Right=�6r   $��50��6�   ��68   �� $  perscnt1�$$ 
{0: #,##0}��6�   5�   +   5�   '   5�        5�      $	 100 65    5�   "   �� 5�   &   $��Amount��60   $��80��6>   $ Right=�6I   $��12(�6_    5�   "   �� 5�   &   .R�5�   *   � .+�5�   .   �� $��90��6s   $ Right=�6~   ��68   �� $  tnetam�$$ {0: #,##0.00}��6~   5�   +   5�   '   5�      5�      5�      5�      5�      5�      5�      