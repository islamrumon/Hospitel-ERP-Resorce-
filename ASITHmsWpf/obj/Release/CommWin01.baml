   M S B A M L   `   `   `  ���� *  &System.Windows.Controls.Layout.ToolkitqUclr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Layout.ToolkitSystem.Windows.Controls  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem E AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ASITHmsWpf
ASITHmsWpf 7 clr-namespace:ASITHmsWpf.GeneralASITHmsWpf.General  	AccordionT8clr-namespace:System.Windows.Controls;assembly=AccordionSystem.Windows.Controls    ASITHmsWpf.CommWin01   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     itoolkitUclr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Layout.Toolkit     9system&clr-namespace:System;assembly=mscorlib     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     *localclr-namespace:ASITHmsWpf     9localGeneral clr-namespace:ASITHmsWpf.General     MControls8clr-namespace:System.Windows.Controls;assembly=Accordion     -     �Name$  MainWindow1�� � Title$. (CentERPoint Healthcare Management System�� � Icon$ Images/Health2.ico��   HmsMainWindowStyle#��[   � 	Resources   acrMainCommand2 �� Text$  ASITHmsWpf.TabControlViewModel 	 tabVM   booleanToVisibilityConverter �� 
TargetType  gr1 �� Property �� Value �� Color  stk1	 �� EndPoint
 �� 
StartPoint � Color � Offset 	 stk01  lbl1  lbl2��%  &	       &	      &	      &	      &	 �     &	 �    &	 <    &	 �    �� $
 Text��  �� �� " �.���  � ��$	 170����  � ��$	 170����  � �� �� $ 	#FF17C0D2����  � ��$ 
0,20,30,10q���  �@�Cursor$
 Hand���� " ��.���  �@��Orientation$ 
Horizontal=���  � ��$ 25����  � ��$	 Top=���  � �� �� $	 0.5,1&�$
 0.5,0&�.��� $ 	#FEC2E61E��$
 0.77Q�� $ 	#FED5ED65��$ 0.494Q��� " ��.���  � ��$
 Left=���  � ��$ 25����  � ��$	 170����  �@��Orientation$ 
Horizontal=���  � ��$ Bottom=���  � ��
 ��ح��� " ��.���  � ��$
 Left=���  � ��$	 148����  � ��$ 2q��� " ��.���  � ��$
 Left=���  � ��$
 Bold"� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command# [  J� InputBindings �� 
 �� Key$
 Left�� �� 	Modifiers$ Shift+Controlf� �� Command# [  �� CommandParameter$ ZoomIn���� $ Right��$ Shift+Controlf�# [ $ ZoomOut��.��� -    �Name$ Grid1��$��0,0,0,0q� J� RenderTransformOrigin$	 0,0&����	#FFE9F5E9$	��Top=�$	��708��.+��� -    ��Name$ lblWait1��$��Please Wait.........��$y�Hidden=�$��24(�$	��272��$��50��$��Center=�$��Center=�&   System.Windows.Media.TextOptions   TextHintingMode$ Animated=�   TextFormattingMode$ Display=�$��	ExtraBold"����	#FFF20A0A$��488,301,440,357q�� -   $ VMGrid1��$
��Left=�$��0,0,0,0q�$	��Top=�$	��250��$	��700��$	 0,0&����� $	 0.5,1&�$
 0.5,0&�.��� $ Black��� $ 	#FF17C0D2��$ 0.002Q�� -   $ UcGrid1��$
��Left=�$y�Hidden=�$	��Top=�$
��1200��$	��650��$	 0,0&�$��0,90,0,0q�.+��� $��0q� �� CornerRadius$ 8�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$ 5Q�   Color$ Black��   Opacity$ .8Q�   
BlurRadius$ 20Q�   RenderingBias$ Quality=����� $		 0,1&�$	
 0,1&�.��� $ 	#FFECFBFB��$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF5F6E6E��$	 1.0Q����� $	 0.5,1&�$
 0.5,0&�.��� $ 	#FFF7F7F7��$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF22F5F5��$	 1.0Q��� -   $ 	lblTitle1��$��Title of the screen��$
��Left=�$	��Top=�$
��1200��$��35��$��Center=�$��20(����	#FF862222  � ForceCursor  . $
��Bold"�$��Verdana)�$��0,0,0,0q����� $	 0.5,1&�! � MappingMode$! RelativeToBoundingBox=�$
 0.5,0&�.��� $ White��� $ 	#FFB7B7B7��$
 0.46Q�� $ 	#FFDDDDDD��$ 0.233Q��� -   " ��Name$" btnPrint�� 	 btnPrint#��� 	 $��0q�
������ |�.  $y�Hidden=�$��Right=�$��	0,0,113,0q�$	��Top=�$��32��$��32���� -	   $" btnClose��$��_Close��|�.  $��Right=�$��0,3,10,0q�$	��Top=�$��75��$��16(�$��Verdana)� 
 StdBtnStyle01#��[
 $��25��# � ToolTip$)# #Click to close the screen (Alt + C)��� -
   $ HMGrid1��$
��Left=�$	��Top=�$
��1200��$��80������ $		 1,1&�$	
 0,0&�.��� $ 	#FFABF59F��$ 1Q�� $ 	#FF32B20B��$	 0.5Q�� $ 	#FF26A000��$ 0Q�.+��� -   $ ��Name$$ 	imgClIcon��$
��Left=�$	��110��$��Images/Digilab_icon.png��$
��Fill=�$��10,-10,0,-10q��� -   $$ 
imgClTitle��$��	125,0,0,0q�$
��Left=�$��80��$ ��Images/Digilab_banner1.png��$
��Fill=�$	��507���� % �� Orientation$% 
Horizontal=�$��0,0,0,54q�$��Right=�.+��� -   $" btnMinimize��#��[
 $��	_Minimize��$��66��|�.  $"# Minimize Application (Alt+M)��$��23��$��0,1,0,0q��� -   $" BtnAppClode��#��[
 $��E_xit��$��60��$��Bookman Old Style)�$## Exit From Application (Alt+X)��$��16(�|�.  $��0,0,0,0q�$��23��$��	ExtraBold"��� $��Right=�$��Bottom=�$��54��$	��169��$��Images/CentERPoint3.png��$
��Fill=��� $��80��$��Right=�$	��430��$% 
Horizontal=�.+��� $
��Left=�$% Vertical=�$��0,0,0,0q�$	��180��.+��� $% 
Horizontal=�.+��� -   $ lblSignInNamTitle�����	#FF000CFF$��Verdana)�$��12(�$��65��$��Right=�$��Sign In:���� -   $ lblSignInNam�����	#FF000CFF$��Verdana)�$��12(�$	��115��$
��Left=�$��HAFIZ���� $% 
Horizontal=�.+��� -   $ lblSessionIDTitle�����	#FF000CFF$��Verdana)�$��12(�$��65��$��Right=�$��Session:���� -   $ lblSessionId�����	#FF000CFF$��Verdana)�$��12(�$	��115��$
��Left=�$��526586���� $% 
Horizontal=�.+��� -   $ lblTeminalIDTitle�����	#FF000CFF$��Verdana)�$��12(�$��65��$��Right=�$��	Terminal:���� -   $ lblTeminalId�����	#FF000CFF$��Verdana)�$��12(�$	��115��$
��Left=�$��UNKNOWN���� -   $" 
btnSignOut��#��[
 |�.  $��11(�$y�Hidden=�$
��Left=�$��Bottom=�$��20��$��55��$��	Si_gn Out���� -   $$ imgSignInUser��$��80��$��5,0,0,0q�$��images/NoUser.jpg��$
��Fill=�$��75��� -   $ MMGrid1��$��
250,80,0,0q�$y�Visible=����� -   imgBack��& �� ImageSource$& /Images/dashbordpic.jpg��.+��� -   ' ��Name$
' stk1��$��Center=�$	��Top=�$
��1000��$��0,60,0,0q�z�T� -   SlideIn��( T� Y$	( 750Q�) � Triggers) :� * :� RoutedEvent$* StackPanel.Loaded��.[��� .���� .��U� + �� 
TargetName$+ SlideIn��, �� TargetProperty$, Y�.\��� - O� KeyTime$- 0:0:1.25��. O� Value$. 0Q�U� $
+ stk1��$, Opacity�.\��� $- 0:0:1.55��$. 1Q�.+��� .+��� -   $$ imgFrontDesk��$��/Images/dashbordpic.jpg��$y�	Collapsed=�$��0��$��0���� -   $$ imgMedStore��$��/Images/MedShop1.jpg��$y�	Collapsed=�$��0��$��0���� -   $$ imgDocVisit��$��/Images/DocVisit1.png��$y�	Collapsed=�$��0��$��0���� $% 
Horizontal=�$��55��$	��Top=�$��Center=�$��0,0,0,0q�   $ 0.595Q�$ 	#FF040404��$ Quality=�.+��� $��/Images/CentERPoint3.png��$��50���� $��Center=�$	��Top=�$	��970��$��	-28,0,0,0q�.+��� $��Center=�$��	Main Menu��
�������/ J� Opacity$/ .5Q�
���   �$
��Bold"�$��17(�� -   0 �Name$
0 wrp1��
������ $	��400��$��Center=�$
��1000��) :� $* Loaded��.[��� .���� 1 g� Duration$1 00:00:6D�$, Opacity�.��W� 2 W� From$2 0X�	3 W� To$3 1X�   $
 0.44Q�.+�� -   $ Grid01��
4 � Tag$4 01��#��[ ) :� $* Grid.MouseEnter��.[��� .���� $1 00:00:3D�$, Opacity�.��W� $
2 0.25X�$3 1X�.+��� .+��� #��[ .+��� $��01��
����  �$
��Bold"��� -    $ 
lblTitle01��$��Optional 01��#��[ �� $	��170��.+��� $��/Images/Reception.png��$��80���� $��40���� -!   $' acriFrontDesk1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=�� 5 � AllowsTransparency5 . $��LeftToRight=�$��Slide=�$	��Top=����� 6 �� ElementName$6 acriFrontDesk1��7 �� Path$7 IsMouseOver�8 �� Mode$8 OneWay=�9 � PlacementTarget9 �� $6 acriFrontDesk1��.���� -"   : ��Name$: txtbl01��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	/ 0.5Q�
�������$��
ExtraLight"�; �� Padding$; 5q�$	��170��$
��Wrap=�� -#   $ Grid02��$4 02��#��[ ) :� $* Grid.MouseEnter��.[��� .���� $1 00:00:3D�$, Opacity�.��W� $
2 0.25X�$3 1X�.+��� .+��� #��[ .+��� $��02��
����  �$
��Bold"��� -$   $ 
lblTitle02��$��Optional 02��#��[ �� $��0,5,0,0q�.+��� $��/Images/inventory.png��$��80���� $��35���� -%   $' acriLab1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=�� 5 . $��LeftToRight=�$��Slide=�$	��Top=����� $6 acriLab1��$7 IsMouseOver�$8 OneWay=�9 �� $6 acriLab1��.���� -&   $: txtbl02��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	/ 0.5Q�
�������$��
ExtraLight"�$; 5q�$	��170��$
��Wrap=�� -'   $ Grid03��$4 03��#��[ ) :� $* Grid.MouseEnter��.[��� .���� $1 00:00:3D�$, Opacity�.��W� $
2 0.25X�$3 1X�.+��� .+��� #��[ .+��� $��03��
����  �$
��Bold"��� -(   $ 
lblTitle03��$��Optional 03��#��[ �� $��0,6,0,0q�.+��� $��/Images/reportdel.png��$��80���� $��35���� -)   $' 
acriStore1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=�� 5 . $��LeftToRight=�$��Slide=�$	��Top=����� $6 
acriStore1��$7 IsMouseOver�$8 OneWay=�9 �� $6 
acriStore1��.���� -*   $: txtbl03��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	/ 0.5Q�
�������$��
ExtraLight"�$; 5q�$	��170��$
��Wrap=�� -+   $ Grid04��$4 04��#��[ ) :� $* Grid.MouseEnter��.[��� .���� $1 00:00:3D�$, Opacity�.��W� $
2 0.25X�$3 1X�.+��� .+��� #��[ .+��� $��04��
����  �$
��Bold"��� -,   $ 
lblTitle04��$��Optional 04��#��[ �� $��0,6,0,0q�.+��� $��/Images/report4.png��$��80��$
��Fill=�$��95���� $��35���� --   $' 	acriProc1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=�� 5 . $��LeftToRight=�$��Slide=�$	��Top=����� $6 	acriProc1��$7 IsMouseOver�$8 OneWay=�9 �� $6 	acriProc1��.���� -.   $: txtbl04��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	/ 0.5Q�
�������$��
ExtraLight"�$; 5q�$	��170��$
��Wrap=�� -/   $ Grid05��$4 05��#��[ ) :� $* Grid.MouseEnter��.[��� .���� $1 00:00:3D�$, Opacity�.��W� $
2 0.25X�$3 1X�.+��� .+��� #��[ .+��� $��05��
����  �$
��Bold"��� -0   $ 
lblTitle05��$��Optional 05��#��[ �� $��0,6,0,0q�.+��� $��/Images/FL4.png��$��80���� $��35���� -1   $' acriAcc1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=�� 5 . $��LeftToRight=�$��Slide=�$	��Top=����� $6 acriAcc1��$7 IsMouseOver�$8 OneWay=�9 �� $6 acriAcc1��.���� -2   $: txtbl05��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	/ 0.5Q�
�������$��
ExtraLight"�$; 5q�$	��170��$
��Wrap=�� -3   $ Grid06��$4 06��#��[ ) :� $* Grid.MouseEnter��.[��� .���� $1 00:00:3D�$, Opacity�.��W� $
2 0.25X�$3 1X�.+��� .+��� #��[ .+��� $��06��
����  �$
��Bold"��� -4   $ 
lblTitle06��$��Optional 06��#��[ �� $��0,5,0,0q�.+��� $��/Images/Account11.png��$��80���� $��35���� -5   $' acriMkt1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=�� 5 . $��LeftToRight=�$��Slide=�$	��Top=����� $6 acriMkt1��$7 IsMouseOver�$8 OneWay=�9 �� $6 acriMkt1��.���� -6   $: txtbl06��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	/ 0.5Q�
�������$��
ExtraLight"�$; 5q�$	��170��$
��Wrap=�� -7   $ Grid07��$4 07��#��[ ) :� $* Grid.MouseEnter��.[��� .���� $1 00:00:3D�$, Opacity�.��W� $
2 0.25X�$3 1X�.+��� .+��� #��[ .+��� $��07��
����  �$
��Bold"��� -8   $ 
lblTitle07��$��Optional 07��#��[ �� $��0,5,0,0q�.+��� $��/Images/bw.png��$��80���� $��35���� -9   $' acriHcm1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=�� 5 . $��LeftToRight=�$��Slide=�$	��Top=����� $6 acriHcm1��$7 IsMouseOver�$8 OneWay=�9 �� $6 acriHcm1��.���� -:   $: txtbl07��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	/ 0.5Q�
�������$��
ExtraLight"�$; 5q�$	��170��$
��Wrap=�� -;   $ Grid08��$4 08��#��[ ) :� $* Grid.MouseEnter��.[��� .���� $1 00:00:3D�$, Opacity�.��W� $
2 0.25X�$3 1X�.+��� .+��� #��[ .+��� $��08��
����  �$
��Bold"��� -<   $ 
lblTitle08��$��Optional 08��#��[ �� $��0,5,0,0q�.+��� $��/Images/item3.png��$��80���� $��35���� -=   $' 
acriAdmin1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=�� 5 . $��LeftToRight=�$��Slide=�$	��Top=����� $6 
acriAdmin1��$7 IsMouseOver�$8 OneWay=�9 �� $6 
acriAdmin1��.���� ->   $: txtbl08��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	/ 0.5Q�
�������$��
ExtraLight"�$; 5q�$	��170��$
��Wrap=�� -?   $ Grid09��$4 09��#��[ ) :� $* Grid.MouseEnter��.[��� .���� $1 00:00:3D�$, Opacity�.��W� $
2 0.25X�$3 1X�.+��� .+��� #��[ .+��� $��09��
����  �$
��Bold"��� -@   $ 
lblTitle09��$��Optional 09��#��[ �� $��0,5,0,0q�.+��� $��/Images/file.png��$��80���� $��35���� -A   $' 
acriSetup1��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=�� 5 . $��LeftToRight=�$��Slide=�$	��Top=����� $6 
acriSetup1��$7 IsMouseOver�$8 OneWay=�9 �� $6 
acriSetup1��.���� -B   $: txtbl09��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	/ 0.5Q�
�������$��
ExtraLight"�$; 5q�$	��170��$
��Wrap=�� -C   $ Grid10��$4 10��#��[ ) :� $* Grid.MouseEnter��.[��� .���� $1 00:00:3D�$, Opacity�.��W� $
2 0.25X�$3 1X�.+��� .+��� #��[ .+��� $��10��
����  �$
��Bold"��� -D   $ 
lblTitle10��$��Optional 10��#��[ �� $��0,5,0,0q�.+��� $ ��/Images/contracts_icon.png��$��80���� $��35���� -E   $' 
acriSetup2��#��[ .+��� #��[ �� $��21��$��/Images/Arrows-icon.png��$��Right=�� 5 . $��LeftToRight=�$��Slide=�$	��Top=����� $6 
acriSetup2��$7 IsMouseOver�$8 OneWay=�9 �� $6 
acriSetup2��.���� -F   $: txtbl10��$F��@The seething sea ceaseth and thus the seething sea sufficeth us.��
���   �$	/ 0.5Q�
�������$��
ExtraLight"�$; 5q�$	��170��$
��Wrap=�� -G   $ DevGrid1��$
��Left=�
�������$	��Top=�$
��1200��$��20��$	 0,0&�$��0, 750, 0, -70q�.+��� $��1��$	��Top=�
��� � ��� $% 
Horizontal=�.+��� $% 
Horizontal=�.+��� $
��ASIT��|�.  #��[
 $��80���� $��50,2,0,0q�$% 
Horizontal=�.+��� $��12(�$��©��$��Arial Unicode MS)��� $��11(�$L��F 2014-2015 Advanced Software and IT Services Ltd. All rights reserved.��$��Arial Unicode MS)��� $��50��$��10,2,0,0q�.+��� $��11(�
����  �$��Contact:��$��Right=�$��Arial Unicode MS)��� $��5,2,0,0q�.+��� $
��Left=�$��11(�$��Arial Unicode MS)�$1��+Phone: (+88)-02-9116439, 9143472, 9118665, ��
E-Mail: �� -H   
����  ���.  $��mailto:info@asit.com.bd?�.�info@asit.com.bd	, Web: �� -I   
����  ���.  $��http://www.asit.com.bd?�.�www.asit.com.bd]� $��Right=�$	��350��.+��� -J   < ��Name$< slider1��$	��180��$
��Left=�= � Maximum$	= 1.5Q�> � Minimum$> 1Q�? �� TickFrequency$	? 0.2Q�@ �� TickPlacement$@ TopLeft=�|�.  A � ValueA �� $6 UcGrid1��$7 LayoutTransform.ScaleX�$8 OneWay=��� -K   $: tbArrivalDateTime��$��Right=�$	��150��$��11(�$��Right=�$��0,2,0,0q�$	��Top=�(  "System.Windows.Controls.DatePicker  -L   B  Name$B dtpMW1��$��Right=�|�.  $y�Hidden=�$��30��$��12��