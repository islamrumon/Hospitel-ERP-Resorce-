   M S B A M L   `   `   `  ����   
ASITHmsWpf    ASITHmsWpf.HmsSignIn1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  
HmsSignIn1�� � WindowStartupLocation$ CenterScreen=�$	��225��$	��450�� � WindowStyle$
 None=� � Icon$ Images/Health2.ico�� � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � Triggers :�  :� RoutedEvent$ Loaded��.[��� .����  g� Duration$ 00:00:6D�	 �� TargetProperty$	 Opacity�.��W� 
 W� From$
 0X�	 W� To$ 1X�.��� .+��� 
�������$��Bottom=�$	��150��.+��� -    ��Name$ txblMsg1��$��22��$��Center=�$
��Bold"�$8��2Sign In information is not valid. Please try again��$��14(�|�.  
���  ����  �� Orientation$ Vertical=�$��Center=�$��0,0,0,0q�$��Center=�.+��� $ 
Horizontal=�.+��� $��
User Name ��$
��Left=�$	��120��$
��Bold"� �� Target ��  �� ElementName$ txtreqRefpr���� $��:��$
��Bold"��  � RowDefinitions �� .+�(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ txtSignInName��$��0�� 
  wtxtBx#��[     	Watermark$ Type User Name��$��5,0,0,0q� �� TextWrapping$
 Wrap=�$�� ��$
��Left=�$��25��$	��250��$��14(��� -    ��Name$ 
btnReferAp��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ Vertical=�.+��� $��10��$��5��$y�Hidden=��� $ 
Horizontal=�.+��� $��User Password��$
��Left=�$	��120��$
��Bold"� �� $ txtreqRefpr���� $��:��$
��Bold"�G� -    G�Name$ PasswordBox1��$	��250��$��16(��� $ 
Horizontal=�$��Right=�$��0,10,0,0q�.+��� -    ��Name$ ChkPass��$��Change _Password��$��15��$	��120��|�.  �� $��70��$y�Hidden=��� -    ��Name$ 	stkbutton��$ 
Horizontal=�.+��� -   $ 	btnSingIn��
������ $��80�����	#FFDC1111$��_Sign In��$��25�� � Cursor$
 Hand��$
��Bold"� �� 	IsDefault . �� $��30��$y�Hidden=��� -	   $ 	btnCancel��
������ $��80��$��_Cancel��|�.  $��25�����	#FFDC1111 �� IsCancel . $
��Bold"��  � HorizontalOffset$	 -37�� � AllowsTransparency . $��Slide=����� *	IsChecked+$ ChkPass�� � PlacementTarget �� $ ChkPass��.���� 
�������$	��450��$	��130��$��0,10,0,0q�z��� -
   theTransform2��.+��� $��	35,10,0,0q�.+��� $ 
Horizontal=�.+��� $��New Password ��$
��Left=�$	��120��$
��Bold"� �� $ txtreqRefpr���� $��:��$
��Bold"��  �� .+�G� -   $ NewPasswordBox1��$	��250��$��16(��� $ Vertical=�.+��� $��10��$��5��$y�Hidden=��� $ 
Horizontal=�.+��� $��Confirm Password��$
��Left=�$	��120��$
��Bold"� �� $ txtreqRefpr���� $��:��$
��Bold"�G� -   $ NewPasswordBox2��$	��250��$��16(��� $ 
Horizontal=�$��Right=�$��0,10,0,0q�.+��� $��55��$y�Hidden=��� -   $ 	btnSubmit��
������ $��80�����	#FFDC1111$��_Submit��$��24��$
 Hand��$
��Bold"� . �� $��20��$y�Hidden=��� -   $ 
btnCancel1��
������ $��80��$��_Cancel��|�.  $��24�����	#FFDC1111 . $
��Bold"��� $	��Top=�$��70��$ 
Horizontal=� J� RenderTransformOrigin$ 0.5,0.5&�����  �� EndPoint$ 0.5,1&�  � MappingMode$  RelativeToBoundingBox=�! �� 
StartPoint$! 0.5,0&�" �� RelativeTransform" V� .~��� # �� ScaleY$# 1Q�$ �� ScaleX$$ -1Q��� % �� AngleY$% 0Q�& �� AngleX$& 0Q��� ' �� Angle$' 0Q�T� .��� ( � Color$( 	#FF00FF07��) � Offset$) 0.043Q�� $( 	#FF10DB1D��$) 0.617Q�.+��� $	��150��.+��� $	��104��$
��Left=�$��0,0,0,0q�$��images/Medical.png��$	��141���� $��Right=�$	��300��.+��� $��52��$��Right=�$��images/CentERPoint3.png��$	��166���� $"��Healthcare Management System��$��Right=�$	��Top=����	#FF0F43F5$��27��$
��Bold"�$��	0,-15,0,0q�$��14(�