   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ASITHmsWpf
ASITHmsWpf  &     ASITHmsWpf.General.mnuFlowChart1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     *localclr-namespace:ASITHmsWpf       � 	Resources  $   ASITHmsWpf.TabControlViewModel 	  tabVM   booleanToVisibilityConverter �� 
TargetType  gr1 �� Property �� Value �� Color  stk1 �� EndPoint �� 
StartPoint � Color � Offset 	 stk01  lbl1  lbl2��%   &	        &	      &	 
     &	 �     &	 ~    &	 ,    &	 }      �� �� " �.���  � ��$	 170����  � ��$	 170����  � �� �� $ 	#FF17C0D2����  � ��$ 
0,20,30,10q���  �@�Cursor$
 Hand���� " ��.���  �@��Orientation$ 
Horizontal=���  � ��$ 25����  � ��$	 Top=���  � �� �� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FEC2E61E��$
 0.77Q�� $ 	#FED5ED65��$ 0.494Q��� " ��.���  � ��$
 Left=���  � ��$ 25����  � ��$	 170����  �@��Orientation$ 
Horizontal=���  � ��$ Bottom=���  � ��
 ��ح��� " ��.���  � ��$
 Left=���  � ��$	 148����  � ��$ 2q��� " ��.���  � ��$
 Left=���  � ��$
 Bold"�	 � Triggers	 :� 
 :� RoutedEvent$
 Loaded��.[��� .����  g� Duration$ 00:00:6D� �� TargetProperty$ Opacity�.��W�  W� From$ 0X�	 W� To$ 1X�.��� .+�� -    �Name$ gridMainCtrl��z�T� -   SlideIn�� T� Y$	 750Q�	 :� $
 Grid.Loaded��.[��� .���� .��U�  �� 
TargetName$ SlideIn��$ Y�.\���  O� KeyTime$ 0:0:1.25�� O� Value$ 0Q�U� $ gridMainCtrl��$ Opacity�.\��� $ 0:0:1.55��$ 1Q�.+��� -    ��Name$
 stk1��$��Center=�$	��Top=�$
��1000��$��0,50,0,0q�.+��� .+��� -    ��Name$ imgFrontDesk��$��/Images/dashbordpic.jpg��$y�	Collapsed=�$��0��$��0���� -   $ imgMedStore��$��/Images/MedShop1.jpg��$y�	Collapsed=�$��0��$��0���� -   $ imgDocVisit��$��/Images/DocVisit1.png��$y�	Collapsed=�$��0��$��0����  �� Orientation$ 
Horizontal=�$��55��$	��Top=�$��Center=�$��0,0,0,0q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Opacity$ 0.595Q�   Color$ 	#FF040404��   RenderingBias$ Quality=�.+��� $��/Images/CentERPoint3.png��$��50���� $��Center=�$	��Top=�$	��970��$��	-28,0,0,0q�.+��� -    ��Name$ lblMenuTite��$��Center=�$��	Main Menu��
������� J� Opacity$ .5Q�
���   �$
��Bold"�$��17(�� -    �Name$ wrp1a��
������ $	��400��$��Center=�$
��1000��	 :� $
 Loaded��.[��� .���� $ 00:00:6D�$ Opacity�.��W� $ 0X�$ 1X�   $
 0.44Q�.+�� -	   $ Grid01��
 � Tag$ 01��#��[ 	 :� $
 Grid.MouseEnter��.[��� .���� $ 00:00:3D�$ Opacity�.��W� $
 0.25X�$ 1X�.+��� .+��� #��[ .+��� $��01��
����  �$
��Bold"��� -
   $ 
lblTitle01��$��Optional 01��#��[ �� $	��170��.+��� -   $ 
imgTitle01��$��/Images/Reception.png��$��80���� $��40���� -   $ acriFrontDesk1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=��  � AllowsTransparency . $��LeftToRight=�$��Slide=�$	��Top=�����   �� ElementName$  acriFrontDesk1��! �� Path$! IsMouseOver�" �� Mode$" OneWay=�# � PlacementTarget# �� $  acriFrontDesk1��.���� -   $ ��Name$$ txtbl01��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	 0.5Q�
�������$��
ExtraLight"�% �� Padding$% 5q�$	��170��$
��Wrap=�� -   $ Grid02��$ 02��#��[ 	 :� $
 Grid.MouseEnter��.[��� .���� $ 00:00:3D�$ Opacity�.��W� $
 0.25X�$ 1X�.+��� .+��� #��[ .+��� $��02��
����  �$
��Bold"��� -   $ 
lblTitle02��$��Optional 02��#��[ �� $��0,5,0,0q�.+��� -   $ 
imgTitle02��$��/Images/inventory.png��$��80���� $��35���� -   $ acriLab1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=��  . $��LeftToRight=�$��Slide=�$	��Top=����� $  acriLab1��$! IsMouseOver�$" OneWay=�# �� $  acriLab1��.���� -   $$ txtbl02��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	 0.5Q�
�������$��
ExtraLight"�$% 5q�$	��170��$
��Wrap=�� -   $ Grid03��$ 03��#��[ 	 :� $
 Grid.MouseEnter��.[��� .���� $ 00:00:3D�$ Opacity�.��W� $
 0.25X�$ 1X�.+��� .+��� #��[ .+��� $��03��
����  �$
��Bold"��� -   $ 
lblTitle03��$��Optional 03��#��[ �� $��0,6,0,0q�.+��� -   $ 
imgTitle03��$��/Images/reportdel.png��$��80���� $��35���� -   $ 
acriStore1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=��  . $��LeftToRight=�$��Slide=�$	��Top=����� $  
acriStore1��$! IsMouseOver�$" OneWay=�# �� $  
acriStore1��.���� -   $$ txtbl03��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	 0.5Q�
�������$��
ExtraLight"�$% 5q�$	��170��$
��Wrap=�� -   $ Grid04��$ 04��#��[ 	 :� $
 Grid.MouseEnter��.[��� .���� $ 00:00:3D�$ Opacity�.��W� $
 0.25X�$ 1X�.+��� .+��� #��[ .+��� $��04��
����  �$
��Bold"��� -   $ 
lblTitle04��$��Optional 04��#��[ �� $��0,6,0,0q�.+��� -   $ 
imgTitle04��$��/Images/report4.png��$��80��$
��Fill=�$��95���� $��35���� -   $ 	acriProc1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=��  . $��LeftToRight=�$��Slide=�$	��Top=����� $  	acriProc1��$! IsMouseOver�$" OneWay=�# �� $  	acriProc1��.���� -   $$ txtbl04��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	 0.5Q�
�������$��
ExtraLight"�$% 5q�$	��170��$
��Wrap=�� -   $ Grid05��$ 05��#��[ 	 :� $
 Grid.MouseEnter��.[��� .���� $ 00:00:3D�$ Opacity�.��W� $
 0.25X�$ 1X�.+��� .+��� #��[ .+��� $��05��
����  �$
��Bold"��� -   $ 
lblTitle05��$��Optional 05��#��[ �� $��0,6,0,0q�.+��� -   $ 
imgTitle05��$��/Images/FL4.png��$��80���� $��35���� -    $ acriAcc1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=��  . $��LeftToRight=�$��Slide=�$	��Top=����� $  acriAcc1��$! IsMouseOver�$" OneWay=�# �� $  acriAcc1��.���� -!   $$ txtbl05��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	 0.5Q�
�������$��
ExtraLight"�$% 5q�$	��170��$
��Wrap=�� -"   $ Grid06��$ 06��#��[ 	 :� $
 Grid.MouseEnter��.[��� .���� $ 00:00:3D�$ Opacity�.��W� $
 0.25X�$ 1X�.+��� .+��� #��[ .+��� $��06��
����  �$
��Bold"��� -#   $ 
lblTitle06��$��Optional 06��#��[ �� $��0,5,0,0q�.+��� -$   $ 
imgTitle06��$��/Images/Account11.png��$��80���� $��35���� -%   $ acriMkt1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=��  . $��LeftToRight=�$��Slide=�$	��Top=����� $  acriMkt1��$! IsMouseOver�$" OneWay=�# �� $  acriMkt1��.���� -&   $$ txtbl06��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	 0.5Q�
�������$��
ExtraLight"�$% 5q�$	��170��$
��Wrap=�� -'   $ Grid07��$ 07��#��[ 	 :� $
 Grid.MouseEnter��.[��� .���� $ 00:00:3D�$ Opacity�.��W� $
 0.25X�$ 1X�.+��� .+��� #��[ .+��� $��07��
����  �$
��Bold"��� -(   $ 
lblTitle07��$��Optional 07��#��[ �� $��0,5,0,0q�.+��� -)   $ 
imgTitle07��$��/Images/bw.png��$��80���� $��35���� -*   $ acriHcm1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=��  . $��LeftToRight=�$��Slide=�$	��Top=����� $  acriHcm1��$! IsMouseOver�$" OneWay=�# �� $  acriHcm1��.���� -+   $$ txtbl07��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	 0.5Q�
�������$��
ExtraLight"�$% 5q�$	��170��$
��Wrap=�� -,   $ Grid08��$ 08��#��[ 	 :� $
 Grid.MouseEnter��.[��� .���� $ 00:00:3D�$ Opacity�.��W� $
 0.25X�$ 1X�.+��� .+��� #��[ .+��� $��08��
����  �$
��Bold"��� --   $ 
lblTitle08��$��Optional 08��#��[ �� $��0,5,0,0q�.+��� -.   $ 
imgTitle08��$��/Images/item3.png��$��80���� $��35���� -/   $ 
acriAdmin1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=��  . $��LeftToRight=�$��Slide=�$	��Top=����� $  
acriAdmin1��$! IsMouseOver�$" OneWay=�# �� $  
acriAdmin1��.���� -0   $$ txtbl08��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	 0.5Q�
�������$��
ExtraLight"�$% 5q�$	��170��$
��Wrap=�� -1   $ Grid09��$ 09��#��[ 	 :� $
 Grid.MouseEnter��.[��� .���� $ 00:00:3D�$ Opacity�.��W� $
 0.25X�$ 1X�.+��� .+��� #��[ .+��� $��09��
����  �$
��Bold"��� -2   $ 
lblTitle09��$��Optional 09��#��[ �� $��0,5,0,0q�.+��� -3   $ 
imgTitle09��$��/Images/file.png��$��80���� $��35���� -4   $ 
acriSetup1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=��  . $��LeftToRight=�$��Slide=�$	��Top=����� $  
acriSetup1��$! IsMouseOver�$" OneWay=�# �� $  
acriSetup1��.���� -5   $$ txtbl09��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	 0.5Q�
�������$��
ExtraLight"�$% 5q�$	��170��$
��Wrap=�� -6   $ Grid10��$ 10��#��[ 	 :� $
 Grid.MouseEnter��.[��� .���� $ 00:00:3D�$ Opacity�.��W� $
 0.25X�$ 1X�.+��� .+��� #��[ .+��� $��10��
����  �$
��Bold"��� -7   $ 
lblTitle10��$��Optional 10��#��[ �� $��0,5,0,0q�.+��� -8   $ 
imgTitle10��$ ��/Images/contracts_icon.png��$��80���� $��35���� -9   $ 
acriSetup2��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=��  . $��LeftToRight=�$��Slide=�$	��Top=����� $  
acriSetup2��$! IsMouseOver�$" OneWay=�# �� $  
acriSetup2��.���� -:   $$ txtbl10��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	 0.5Q�
�������$��
ExtraLight"�$% 5q�$	��170��$
��Wrap=�