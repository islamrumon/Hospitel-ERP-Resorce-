   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      'clr-namespace:ASITHmsWpf
ASITHmsWpf  5      )    #ASITHmsWpf.Manpower.frmEntryAttn102   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      $localclr-namespace:ASITHmsWpf  5      -   5        
  � Tag$D  >Staff Machine Based Attendance Information - Entry/Edit Screen��5	          IOUserControl#��[  6e   .��5
      � .+�5   
   �� $��0,5,0,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5         ��Name$ stkpMain��5      $ Vertical=�6/   .+�5      �� $ 
Horizontal=�6!   .+�5      �� $��
Location :��6    $��80��65   $��Right=�6A   $
��Bold"�6d   �� 5      -   5         ��Name$ cmbSectCodAll��5   #   $	��300��6:    �� 
IsReadOnly .  �� SelectedIndex$ 0��6X   6j   �� 5      $��Month :��6    $��60��62   $��Right=�6=   $
��Bold"�6`   �� 5      -   5        $ cmbInfoMonth��5   #   $	��180��69   $
��Bold"�6E   6W   �� 5      $��Date :��6    $��Right=�61   $��Normal$�6T   $��60��6�   $
��Bold"�6�    �� Target 6g   ��  �� ElementName$ xctk_EventDateAll��6�   &   Xceed.Wpf.Toolkit.DateTimePicker  5      -   5          Name$ xctk_InfoDate��5   .     StdDateTimePickerStyle01#��[ 6F   $	��130��6x   $
��Bold"�6�   5      �� 5      -   5        $ stkpEmpSearch��5   !   $ 
Horizontal=�68   $��0,5,0,0q�6Q   .+�5      �� $��
Employee :��6    $��80��65   $��Right=�6@   $
��Bold"�6c   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  5      -   5        	  Name$	 
AtxtEmpAll��5   1   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox
   MouseDoubleClick$!
 AtxtEmpAll_MouseDoubleClick��6�      
SearchType$ C��6E   $	��540��6T    � ToolTip$= 7Right Click or Double Click to view existing staff list��6`    � ContextMenu 5      �� 5   "   -   5         ��Name$ conMenuHCMAtnAll��5   .   $	��400��6H   $	��400��6U   5      �� 5      -	   5         ��Name$ chkMacAttnDate��5   #   $��Consider Whole Month��6;   $��Center=�6Z   $��30,0,0,0q�6u   5      5      �� 5       $ Vertical=�6   $��25,0,0,0q�64   .+�5!      �� $ 
Horizontal=�6!   .+�5"      �� -
   5         ��Name$ btnShowInfo��5"   !   $
��Show��66   $��75��6E   $��14(�6P   $��24��6^   $��Verdana)�6j     StdBtnStyle01#��[ 6   $��0,0,0,0q�6�   �� 5#      -   5        $ btnPrintAttSchdlDay��5#   !   $��Print��6>   $��75��6N   $��Right=�6Y   $��14(�6u   $��24��6�   #��[ 6�   $��10,0,0,0q�6�   5$      �� 5%      $ 
Horizontal=�6!   $��0,10,0,0q�6:   .+�5&      �� -   5        $ btnUpdateMacData��5&   !   $��Update Machine Data��6;   #��[ 6Y   $��14(�6�   $��24��6�   $	��160��6�   6�   5'      5)      5*      �� 5+      -   5        $ 
stkpMacAtt��5+      $��	10,10,0,0q�6-   .+�5,      &   System.Windows.Controls.DataGrid  -   5          Name$ dgActTrmAttn��5,         AutoGenerateColumns .  $	��480��6M   $
��Left=�6^      
IsReadOnly .    RowHeaderWidth$ 5Q�6�   $��12(�6�    �� AlternationCount$ 2��6�      AlternatingRowBackground �	#FFEDFFEE � 	Resources 5-      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value*  $System.Windows.Controls.DataGridCell �� Triggers P� Property P� Value)  #System.Windows.Controls.DataGridRow��%�  '        '  �     '  H    �� 5.      "  6    .�5/      ��  � �� �	#FFE2D2CE6;   �� 50       � ��$
 Bold"�6;   �� 51       � ��$ 25��67   52      �� 53      "  6     54      P� 55   "    �@ 
IsSelected . . �56   &   ��  � ��
 �����6D   �� 57   &    � ��
 �   �6D   58   #   5:      �� 5;      "  6    .�5<      ��  � ��$ 30��67   5=      5>         Columns 5?      0  *System.Windows.Controls.DataGridTextColumn  5@      ,	  &System.Windows.Controls.DataGridColumn 	  Width5
  /System.Windows.Controls.DataGridLengthConverter$ 90
 6-     	  Header$
  Date��68   !   FontSize$! 10Q�6�   " +� IsFocusScope" .  # 	  CanUserSort# .  $ 	  
IsReadOnly$ . % 	  CanUserReorder% .  & 	  CanUserResize& .  1  +System.Windows.Controls.DataGridBoundColumn'   Binding' 6F   �� ( �� Path$( attndate�) �� StringFormat$) {0: dd-MMM-yyyy ddd}��6�     5A      $	 150
 6-   $  
Department��69   $! 10Q�6o   " .  # .  $ . % .  & .  ' 6M   �� $( sectname�6�     5B      $ 50
 6-   $  Emp ID��68   $! 10Q�6i   " .  # .  $ . % .  & .  ' 6H   �� $( staffid�6�     5C      $	 250
 6-   $&   Name of Employee and Designation��69   $! 10Q�6�   " .  # .  $ . % .  & .  ' 6c   �� $( hcnamdsg�6�     5D      $ 70
 6-   $  S.InTime��68   $! 10Q�6k   " .  # .  $ . % .  & .  ' 6J   �� $( InTime1�6�     5E      $ 70
 6-   $  S.Brk.Start��68   $! 10Q�6o   " .  # .  $ . % .  & .  ' 6M   �� $( OutTime1�6�     5F      $ 70
 6-   $  	S.Brk.End��68   $! 10Q�6l   " .  # .  $ . % .  & .  ' 6K   �� $( InTime2�6�     5G      $ 70
 6-   $  	S.OutTime��68   $! 10Q�6m   " .  # .  $ . % .  & .  ' 6K   �� $( OutTime2�6�     5H      $ 80
 6-   $  	Sch. Note��68   $! 10Q�6j   " .  # .  $ . % .  & .  ' 6K   �� $( Rmrks�6�     5I      $	 260
 6-   $  Machine Punch Details��69   $! 10Q�6x   " .  # .  $ . % .  & .  ' 6X   �� $( atndtl�6�   5K      5L      5M      5N      5O      