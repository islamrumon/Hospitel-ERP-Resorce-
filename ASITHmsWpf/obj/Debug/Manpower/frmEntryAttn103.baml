   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      'clr-namespace:ASITHmsWpf
ASITHmsWpf  5      )    #ASITHmsWpf.Manpower.frmEntryAttn103   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      $localclr-namespace:ASITHmsWpf  5      -   5        
  � Tag$!  Manual Approvals Entry/Edit��5	         IOUserControl#��[  6A   .��5
      � .+�5   
   �� $��0,5,0,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5         ��Name$ stkpMain��5      $ Vertical=�6/   .+�5      �� -   5        $ stkpEmpSearch��5   !   $ 
Horizontal=�68   $��0,0,0,0q�6Q   .+�5      �� $��
Employee :��6    $��80��65   $��Right=�6@   $
��Bold"�6c   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  5      -   5          Name$ 
AtxtEmpAll��5   1   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox   MouseDoubleClick$! AtxtEmpAll_MouseDoubleClick��6�      
SearchType$ C��6E   $	��650��6T    � ToolTip$= 7Right Click or Double Click to view existing staff list��6`    � ContextMenu 5      �� 5   "   -   5         ��Name$ conMenuHCMAtnAll��5   .   $	��400��6H   $	��500��6U   5      �� 5      $��Month :��6    $��60��62   $��Right=�6=   $
��Bold"�6`   �� 5      -   5        	 ��Name$	 cmbInfoMonth��5   #   $	��180��69   $
��Bold"�6E   5      5      �� 5      $ 
Horizontal=�6   $��53,0,0,0q�66   .+�5      �� -   5        
 ��Name$
 btnShowInfo��5      $
��Show��62   $��75��6A   $��14(�6L   $��24��6Z   $��Verdana)�6f     StdBtnStyle01#��[ 6{   6�   �� 5      -   5        $
 btnUpdateInfo��5      $��Update��64   $��75��6E   $��14(�6P   $��24��6^   $��Verdana)�6j   #��[ 6   $��10,0,0,0q�6�   5      5      �� 5      $��2��6   �� 5      -	   5        $ stkpApproval��5   *   $��5,0,5,0q�6   $
��Left=�6@   5!      5"      5#      5$      