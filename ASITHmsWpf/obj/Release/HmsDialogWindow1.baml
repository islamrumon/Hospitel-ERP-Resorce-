   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ASITHmsWpf
ASITHmsWpf  !    ASITHmsWpf.HmsDialogWindow1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     *localclr-namespace:ASITHmsWpf     -     � Title$  HmsDialogWindow1��$	��680��$
��1210�� � WindowStartupLocation$ CenterOwner=� � WindowStyle$ SingleBorderWindow=�!   ASITHmsWpf.WindowCustomizer   CanMinimize .     CanMaximize .   � WindowState$ Normal=� � Icon$ Images/Health2.ico�� � 
ResizeMode$ NoResize=� � ShowInTaskbar .  $��Disabled=�.��� .+�� -   	 �Name$	 UcGrid1��$
��Left=�$y�Visible=�$	��Top=�$
��1200��$	��650��
 J� RenderTransformOrigin$	
 0,0&�$��0,0,0,0q�.+��� $��0q� �� CornerRadius$ 8�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$ 5Q�   Color$ Black��   Opacity$ .8Q�   
BlurRadius$ 20Q�   RenderingBias$ Quality=�����  �� EndPoint$	 0,1&� �� 
StartPoint$	 0,1&�.���  � Color$ 	#FFECFBFB�� � Offset$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF5F6E6E��$	 1.0Q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFF7F7F7��$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF22F5F5��$	 1.0Q��� -    ��Name$ 	lblTitle1��$��Title of the screen��$
��Left=�$	��Top=�$
��1200��$��0��$��Center=�$��20(����	#FF862222 � ForceCursor . $
��Bold"�$��Verdana)�$��0,0,0,0q����� $ 0.5,1&� � MappingMode$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ White��� $ 	#FFB7B7B7��$
 0.46Q�� $ 	#FFDDDDDD��$ 0.233Q��� -    ��Name$ btnPrint��   btnPrint#���   $��0q�
������ |�.  $y�Hidden=�$��Right=�$��	0,0,113,0q�$	��Top=�$��32��$��32���� -   $ btnClose��$��_Close��|�.  $��Right=�$��0,3,10,0q�$	��Top=�$��75��$y�Hidden=�$��16(�$��Verdana)�  StdBtnStyle01#��[ $��25�� � ToolTip$) #Click to close the screen (Alt + C)��