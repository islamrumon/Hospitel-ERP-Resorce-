   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      'clr-namespace:ASITHmsWpf
ASITHmsWpf  5      7 clr-namespace:ASITHmsWpf.GeneralASITHmsWpf.General  5	      *    $ASITHmsWpf.General.frmConfigSetup102   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      $localclr-namespace:ASITHmsWpf  5      /localGen clr-namespace:ASITHmsWpf.General  5	      -   5        
  � Tag$  COMPANY INFORMATION��5
         IOUserControl#��[  6V   .��5      � ���	#FFE4F2E1 � ColumnDefinitions 5   
   �� 5      $
��393* �6   �� 5      $
��761* �6   .+�5   
   �� $��	20,10,0,0q�6   $��2��6)   z�5      �� 5      -   5        	noFreeze4��5   "    J� RenderTransformOrigin 5      8� 5       8� X$	 0.5Q�6    8� Y$	 0.5Q�6     � Triggers 5      :� 5       :� RoutedEvent$ Window.Loaded��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$ (StackPanel.Height)�6.    W� From$ 0X�6_   		 W� To$		 650X�6h   
 g� Duration$
 0:0:1D�6q   W� 5      $ (StackPanel.Width)�6.   $ 0X�6^   $
	 1200X�6g   $
 0:0:1D�6q   W� 5      $: 4(StackPanel.RenderTransform).(RotateTransform.Angle)�5   -   $	 360X�5    /   $	 0X�6:   $
 0:0:1D�6A   5!      5"      5#      .+�5%      ��  �� Orientation$ 
Horizontal=�6   .+�5&      �� $��Company Information��6   $
��Left=�66   $	��Top=�6Q   �� 5'      $	��200��6   $y�Hidden=�6(   �� 5(      -   5         ��Name$ chkLogo��5(      $
��Logo��6,   $��Center=�6;   $��80��6V    d� 	IsChecked$
 TrueY�6a   �� 5)      $��20��6   $y�Hidden=�6'   �� 5*      -   5        $ chkLabel��5*      $��Title Label��6-   $��Center=�6C   $	��100��6^   $ FalseY�6j   �� 5+      $��20��6   $y�Hidden=�6'   �� 5,      -   5         ��Name$ btnUpdateCompInfo��5,      $��25��65   $��_Update��6A   $	��100��6S     StdBtnStyle01#��[ 6_   6�   5-      �� 5.      $ 
Horizontal=�6   .+�5/      �� $ Vertical=�6   .+�50      �� $��Company Logo :��6    $
��Left=�66   $��16(�6Q   $��Normal$�6_   �� 51      -   5         ��Name$ imgLogo��51      $��..\Images\blue-bckgrd.jpg��6.   $
��Left=�6Q   $y�Visible=�6l   $
��Fill=�6�   $	��181��6�   $	��166��6�   �� 52      $ 
Horizontal=�6!   $��30��6:   .+�53      �� -   5         ��Name$ txtLogo��53   "   {�.  $��22��6F   $	��240��6R   6�   �� 54      -   5        $ btnUpdateLogo��54   !   $��20��68   $��Choose��6a   $��60��6s   6�   55      56      �� 57      $��	50,50,0,0q�6   .+�58      �� $��Company Info :��6   
����  �$
��Bold"�6G   6[   �� 59      $ 
Horizontal=�6!   .+�5:      �� $��Company Name :��6    $	��150��69   $
��Bold"�6E   6Y   �� 5;      -	   5         ��Name$ cmpname��5;       $�� ��62   6?   5<      �� 5=      $ 
Horizontal=�6!   .+�5>      �� $��	Address :��6    $	��150��64   $
��Bold"�6@   6T   �� 5?      -
   5        $ 
cmpAddrsee��5?       $�� ��65   6B   5@      �� 5A      $ 
Horizontal=�6!   .+�5B      �� $��
Phone No :��6    $	��150��65   $
��Bold"�6A   6U   �� 5C      -   5        $ 
cmpPhoneno��5C       $�� ��65   6B   5D      �� 5E      $ 
Horizontal=�6!   .+�5F      �� $��Email :��6    $	��150��62   $
��Bold"�6>   6R   �� 5G      -   5        $ cmpEmail��5G       $�� ��63   6@   5H      5J      5K      �� 5L      $ Vertical=�6   .+�5M      �� $��Application Title Label :��6   $
��Left=�6=   $��16(�6X   $��Normal$�6f   �� 5N      -   5        $ imgLabel��5N      $��..\Images\blue-bckgrd.jpg��6+   $
��Left=�6N   $y�Visible=�6i   $
��Fill=�6~   $	��586��6�   $	��116��6�   �� 5O      $ 
Horizontal=�6   $��30��66   .+�5P      �� -   5        $ txtLabel��5P      {�.  $��22��6C   $	��240��6O   6�   �� 5Q      -   5        $ btnUpdateLabel��5Q      $��20��65   $��Choose��6C   $��60��6p   $	��0,5q�6{   6�   5R      5S      5T      5U      5V      