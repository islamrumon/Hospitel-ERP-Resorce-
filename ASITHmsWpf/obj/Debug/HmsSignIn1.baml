   M S B A M L   `   `   `  ����  
ASITHmsWpf    ASITHmsWpf.HmsSignIn1   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  
HmsSignIn1��5   	    � WindowStartupLocation$ CenterScreen=�6   $	��330��6A   $	��450��6N    � WindowStyle$
 None=�6Z    � 
ResizeMode$ NoResize=�5   "    � ShowInTaskbar .   � Triggers 5      :� 5   
    :� RoutedEvent$ Loaded��6   .[�5	      �� .��5
      �� .��5      W�  W� From$ -1X�6&   	 W� To$ 1X�60   	 g� Duration$	 00:00:1D�67   
 �� TargetProperty$V
 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�6J   W� 5      $ -1X�6&   $ 1X�60   $	 00:00:1D�67   $V
 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�6J   W� 5      $ 0X�6&   $ 1X�6/   $	 00:00:2D�66   $
 Opacity�6I   5      5      5      z�5      V� 5   
   .~�5      ��  �� ScaleX$ 1Q�6    �� ScaleY$ 1Q�6(   5      .��5      � .+�5   
   �� $	��0.5q�6   
���  ��._�5      �� .+�5      �� $��70��6    �� Orientation$ 
Horizontal=�6)    J� RenderTransformOrigin$ 0.5,0.5&�6B   ��5      �� 5       �� EndPoint$ 0.5,1&�6.    � MappingMode$ RelativeToBoundingBox=�6?    �� 
StartPoint$ 0.5,0&�6c    �� RelativeTransform 5      V� 5   "   .~�5   &   �� $ 1Q�65   $ -1Q�6@   �� 5    &    �� AngleY$ 0Q�64    �� AngleX$ 0Q�6?   �� 5!   &    �� Angle$ 0Q�66   T� 5"   &   5#   #   .��5%      �  � Color$ 	#FF00FF07��6+    � Offset$ 0.043Q�6=   � 5&      $ 	#FF10DB1D��6+   $ 0.617Q�6=   5'      .+�5)      �� $	��110��6!   .+�5*      �� -   5         ��Name$ 	imgClIcon��5*       $��70��63   $
��Left=�6?   $
��Fill=�6Z   $��0,0,0,0q�6j   $��images/ASITLogo3.png��6{   $	��110��6�   5+      �� 5-      $��Right=�6!   $	��340��6=   .+�5.      �� $��52��6    $��Right=�6,   $��images/CentERPoint3.png��6I   $	��166��6k   �� 5/      -   5         ��Name$ lblAppTitle2��5/       $"��Healthcare Management System��66   $��Right=�6]   $	��Top=�6�   ���	#FF0F43F5$��27��50      $
��Bold"�6)   $��	0,-15,0,0q�6;   $��13(�6N   51      52      �� 53      
�������.+�54      �� -   5         ��Name$ txblMsg1��54       $��22��62   $��Center=�6?   $
��Bold"�6\   $8��2Sign In information is not valid. Please try again��6n   $��14(�6�   |�.  
���  ��6�   �� 55      $ Vertical=�6!   $��Center=�68   $��0,0,0,0q�6U   $��Center=�6f   .+�56      �� $ 
Horizontal=�6%   .+�57      �� $��Sign-In ID : ��6%   $��Right=�6=   $	��120��6`   $
��Bold"�6l   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  58      -   5          Name$ txtSignInName��58   4    
  wtxtBx#��[  6K      	Watermark$ Type User Name��6k   $��5,0,0,0q�6�    � ToolTip$ 
Type user ��59   +   $��25��6@   $	��250��6L   $��14(�6X    �� CharacterCasing$ Upper=�6f   5:      �� 5;      $ 
Horizontal=�6%   $��0,10,0,0q�6>   .+�5<      �� $��Password : ��6%   $��Right=�6;   $	��120��6^   $
��Bold"�6j   G� 5=      -   5         G�Name$ PasswordBox1��5=   *   $	��250��6@   $��16(�6L   $��3,0,0,0q�6Z   5>      �� 5?      $ 
Horizontal=�6%   $��Right=�6>   $��0,10,0,0q�6[   .+�5@      �� -   5          ��Name$  ChkPass��5@   '   $��Change _Password��68   $��15��6j   $	��120��6v   |�.  6�   �� 5A      -   5        ! ��Name$! 	stkbutton��5A   )   $ 
Horizontal=�6<   $��50,0,0,0q�6U   .+�5B   "   �� -	   5        " ��Name$" 	btnSingIn��5B   )   
������ $��80��6U   ���	#FFDC1111$��_Sign In��6w   $��25��6�   # � Cursor$
# Hand��6�   $
��Bold"�6�   $ �� 	IsDefault$ . 6�   �� 5C   "   -
   5        $" 	btnCancel��5C   )   
������ $��80��6U   $��_Cancel��6`   |�.  $��25��6�   ���	#FFDC1111% �� IsCancel% . $
��Bold"�5D   S   $��30,0,0,0q�6e   5E      5F      5G      �� 5H      -   5        $! stkpChangePass��5H   !   $��40,0,0,0q�69   .+�5I      �� $ 
Horizontal=�6%   .+�5J      �� $��New Password : ��6%   $��Right=�6?   $	��120��6b   $
��Bold"�6n   G� 5K      -   5        $ NewPasswordBox1��5K   *   $	��250��6C   $��16(�6O   $��3,0,0,0q�6]   5L      �� 5M      $ 
Horizontal=�6%   $��0,10,0,0q�6>   .+�5N      �� $��Confirm Password : ��6%   $
��Left=�6C   $	��120��6e   $
��Bold"�6q   G� 5O      -   5        $ NewPasswordBox2��5O   *   $	��250��6D   $��16(�6P   $��3,0,0,0q�6^   5P      �� 5Q      $ 
Horizontal=�6%   $��Right=�6>   $��	0,10,40,0q�6[   .+�5R      �� -   5        $" 	btnSubmit��5R   %   
������ $��80��6Q   ���	#FFDC1111$��_Submit��6t   $��24��6�   $
# Hand��6�   $
��Bold"�6�   $ . 6�   �� 5S      -   5        $" 
btnCancel1��5S   %   
������ $��80��6R   $��_Cancel��6]   |�.  $��24��6�   ���	#FFDC1111% . $
��Bold"�6�   $��30,0,0,0q�6�   5T      5U      �� 5V      $��0,2,0,0q�6"   .+�5W      �� �� 5X      $��40��6!   $ 
Horizontal=�6-   $��0,-3,0,0q�6F   .+�5Y      �� $��Powered by : ��6$   $��75��69   $
��Left=�6D   $��5,0,0,0q�6_   $
��Bold"�6p   �� 5Z      $��30��6    $
��Left=�6,   $
��Fill=�6G   $��0,0,0,0q�6W   $��images/ASITLogo3.png��6h   $��60��6�   �� 5[      $��5,2,0,0q�6%   .+�5\      �� $*��$Advanced Software & IT Services Ltd.��6(   $	��300��6X   $
��Bold"�6d   $
��Left=�6v   $��14(�6�   �� 5]      $ 
Horizontal=�6)   $��0,-3,0,0q�6B   .+�5^   "   �� $
��Left=�6,   $��12(�6G   
E-Mail: �� 5`      -   5        
����  ���.  $��mailto:info@asit.com.bd?�5`   u   .�6�   info@asit.com.bd6�   
 , Web: �� 5a   %   -   5        
����  ���.  $��http://www.asit.com.bd?�5a      .�6�   �� $�www.asit.com.bd��6�   6�   5b   #   5c      5d      5e      5f      5g      5h      5i      5j      5k      