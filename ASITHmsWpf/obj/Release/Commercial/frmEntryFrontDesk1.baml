   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  =#clr-namespace:ASITHmsWpf.CommercialASITHmsWpf.Commercial  .    (ASITHmsWpf.Commercial.frmEntryFrontDesk1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4UC%clr-namespace:ASITHmsWpf.UserControls     @d2http://schemas.microsoft.com/expression/blend/2008     2tp#clr-namespace:ASITHmsWpf.Commercial     -   
  � Tag$D  >Patient Registration & Service Information Input/Report Screen��   IOUserControl#��[   � 	Resources   FrontDesk1Command1 �� Text  FrontDesk1Command2��%4   &	       &	      �� $
 Text���� $
 Text�� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command# [   -   # [  J� InputBindings �� 
 �� Key$ F3�� �� Command# [  �� CommandParameter$ 0���� $ F4��# [ $ 2���� $ F5��# [ $ 4���� $ F6��# [ $ 6���� $ F7��# [ $ 8���� $ F8��# [ $ 10���� $ F9��# [ $ 12���� $	 F10��# [ $ 14���� 	 �� 	Modifiers$	 Control+Altf�$ PageUp��# [ $ 1���� $	 Control+Altf�$ PageDown��# [ $ 2��.��� .+��� .+��� 
 �� Orientation$
 
Horizontal=�$��40��
�������.+��� -    ��Name$ 	lblTitle1��$��ORDER RECEIPT��$	��750��$��22(�$
��Bold"�$��Center=�$��Center=�$��	220,0,0,0q�	� -    	�Name$ rbtnScreen1��$��Entry Screen-1��$  Entry01��$	��100�� d� 	IsChecked$
 TrueY�$
��Bold"�$��Center=�|�.  	� -   $ rbtnScreen2��$��Entry Screen-2��$  Entry02��$	��100��$��20,0,0,0q�$
��Bold"�$��Center=�|�.  �� -    ��Name$ 	stkpMain1��.+��� -    ��Name$ TabCtrl1��$	��580��
������� �� TabStripPlacement$
 Left=� �� SelectedIndex$ 0��.@��� $	��120��  StdTabItemStyle01#��[ $$  Patient Registration & Billing������ .+��� $
��(F3)��$��20(�$��Center=��� $��Reg. & Billing��$��14(�$��Center=�.���� -	   $ stpkTab0��.+�0   *ASITHmsWpf.Commercial.frmEntryFrontDesk101  -
     Name$ FdEntryScreen1��$��Stretch=�  -   $ FdEntryScreen2��$��Stretch=�$y�	Collapsed=��� $��5��{�.  �� $	��120��#��[ $'  !Report Delivery & Dues Collection������ .+��� $
��(F4)��$��20(�$��Center=��� $��Report Delivery��$��14(�$��Center=�.���� -   $ stpkTab1��.+�0   *ASITHmsWpf.Commercial.frmEntryFrontDesk102  -     Name$ FdEntryDeliveryDues1��$��Stretch=�  -   $ FdEntryDeliveryDues2��$��Stretch=�$y�	Collapsed=��� $��5��{�.  �� $	��120��#��[ $#  Reference Doctors Information������ .+��� $
��(F5)��$��20(�$��Center=��� $��Refd. By List��$��14(�$��Center=�.���� 
�������$��Center=�$��Center=�.+��� $#��Reference Doctors Information���� $��5��{�.  �� $	��120��#��[ $!  Membership Card Information������ .+��� $
��(F6)��$��20(�$��Center=��� $��Members Card��$��14(�$��Center=�.���� 
�������$��Center=�$��Center=�.+��� $.��(Member Registration and Card Information���� $��5��{�.  �� $	��120��#��[ $!  Patient Details Information������ .+��� $
��(F7)��$��20(�$��Center=��� $��Patient Details��$��14(�$��Center=�.���� 
����� �$��Center=�$��Center=�.+��� $!��Patient Details Information���� $��5��{�.  �� $	��120��#��[ $!  Advanced Transaction Search������ .+��� $
��(F8)��$��20(�$��Center=��� $��Adv. Search��$��14(�$��Center=�.���� 
�������$��Center=�$��Center=�.+��� $!��Advanced Information Search���� $��5��{�.  �� $	��120��#��[ $  Operator Reports������ .+��� $
��(F9)��$��20(�$��Center=��� $��Operator Reports��$��14(�$��Center=�.���� 
�������$��Center=�$��Center=�.+��� $��Operator Repors���� $��5��{�.  �� $	��120��#��[ $  Management Reports������ .+��� $��(F10)��$��20(�$��Center=��� $��Mgt. Reports��$��14(�$��Center=�.���� 
�������$��Center=�$��Center=�.+��� $��Management Reports��