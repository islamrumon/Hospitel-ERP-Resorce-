   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ASITHmsWpf.GeneralASITHmsWpf.General  5   	       ASITHmsWpf.HmsMainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0Gen clr-namespace:ASITHmsWpf.General     5   	   -   5          � Title$.  (CentERPoint Healthcare Management System��5   	   $	��800��6:   $
��1200��6G   ���	#FFE9F5E9 � WindowState$ 	Maximized=�6k    � WindowStyle$ SingleBorderWindow=�6�    � 	Resources 5         acrMainCommand2 �� Text  acrMainCommand3��%P   &	        &	      �� 5   
   $
 Text��62   �� 5	   
   $
 Text��62   5
       J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command# [  5      6D     5   
   -   5        # [ 5      6D    J� InputBindings 5      �� 5   
   
 �� Key$
 Left��6   	 �� 	Modifiers$	 Shift+Controlf�6    
 �� Command#
 [  6:    �� CommandParameter$ ZoomIn��6e   �� 5   
   $ Right��6   $	 Shift+Controlf�6!   #
 [  6;   $ ZoomOut��6f   �� 5   
   $ Space��6   $		 Altf�6!   #
 [ 61   $
 NONE��6\   .��5      � .+�5   
   � -   5         �Name$ HMGrid1��5      $��Stretch=�6    $	��Top=�6>   $��50��6W   ��5      �� 5       �� EndPoint$	 1,1&�6&    �� 
StartPoint$	 0,0&�65   .��5      �  � Color$ 	#FFABF59F��6#    � Offset$ 1Q�65   � 5      $ 	#FF32B20B��6#   $	 0.5Q�65   � 5      $ 	#FF26A000��6#   $ 0Q�65   5      .+�5      �� -   5         ��Name$ 	imgClIcon��5      $
��Left=�6'   $��75��6B   $��50��6M   $��Images/ASITLogo3.png��6Y   $
��Fill=�6w   $��0,0,0,0q�6�   �� 5      -   5        $ 
imgClTitle��5      $��	120,0,0,0q�6(   $
��Left=�6<   $��50��6W   $��Images/ASITTitle2.png��6c   $
��Fill=�6�   $	��507��6�   6�   �� 5       �� Orientation$ 
Horizontal=�6   $��0,0,0,30q�62   $��Right=�6D   .+�5"      �� -   5         ��Name$ btnAppClose��5"      $
��Exit��6.   $��60��6=   $��Bookman Old Style)�6b    � ToolTip$$ Exit From Application (Alt+F4)��6�   $��14(�5#      $��0q�6'   |�.  $��	5,-1,10,0q�6E   $��20��6X   $
��Bold"�6d    � Cursor$
 Hand��6v   
���  ��
������ 6�   5$      �� 5%      $��Right=�6   $��Bottom=�60   $��35��6K   $	��169��6W   $��Images/CentERPoint3.png��6c   $
��Fill=�6�   �� 5&      $��50��6   $��Right=�6%   $	��430��6A   $ 
Horizontal=�6M   .+�5'      �� $
��Left=�6   $ Vertical=�68   $��0,0,0,0q�6O   $	��180��6a   .+�5(      �� $ 
Horizontal=�6!   .+�5)      �� -   5         ��Name$ lblSignInNamTitle��5)       ���	#FF000CFF$��Verdana)�6R   $��12(�6g   $��65��6u   $��18��6�   $��Right=�6�   $��Sign In:��6�   $��0,1,2,0q�6�   �� 5*      -	   5        $ lblSignInNam��5*       ���	#FF000CFF$��Verdana)�6M   $��12(�6b   $	��115��6p   $��18��6|   $
��Left=�6�   $��HAFIZ��6�   $��0,1,2,0q�6�   5+      �� 5,      $ 
Horizontal=�6!   $��0,-2,0,0q�6:   .+�5-      �� -
   5        $ lblSessionIDTitle��5-       ���	#FF000CFF$��Verdana)�6R   $��12(�6g   $��65��6u   $��18��6�   $��Right=�6�   $��Session:��6�   $��0,1,2,0q�6�   �� 5.      -   5        $ lblSessionId��5.       ���	#FF000CFF$��Verdana)�6M   $��12(�6b   $	��115��6p   $��18��6|   $
��Left=�6�   $��526586��6�   $��0,1,2,0q�6�   5/      �� 50      $ 
Horizontal=�6!   .+�51      �� -   5        $ lblTeminalIDTitle��51       ���	#FF000CFF$��0,-2,0,0q�6R   $��Verdana)�6d   $��12(�6y   $��65��6�   $��18��6�   $��Right=�6�   $��	Terminal:��6�   $��0,1,2,0q�6�   �� 52      -   5        $ lblTeminalId��52       ���	#FF000CFF$��0,-2,0,0q�6M   $��Verdana)�6_   $��12(�6t   $	��115��6�   $��18��6�   $
��Left=�6�   $��UNKNOWN��6�   $��0,1,2,0q�6�   53      54      �� 55      -   5        $ imgSignInUser��55      $��50��6/   $��5,0,0,0q�6;   $��images/NoUser.jpg��6M   $
��Fill=�6h   $��45��6w   6�   56      57      � 58   
   -   5        $ UcGrid1��58      $��Stretch=�6    $��Stretch=�6>   
������ $��	0,55,0,20q�6s   .+�5:      �� .+�5;      �� -   5         ��Name$ 	txtTitle2��5;      $��	Dashboard��6/   $
��1190��6@    �� TextAlignment$ Center=�6M   $
��Bold"�6d   
���  ��$��14(�6�   $
 Hand��6�   6�   �� 5<      -   5         ��Name$ 
TabUcGrid1��5<      $	��640��61   $
��1200��6>   
���  ��$
��0.25q�6]   
������ .@�5D      �� $��	Dashboard��6     StdTabItemStyle02#��[ 61   $	��100��6\   $
��Bold"�6h   
 � Tag$ 00��6z   .��5E      *   $ASITHmsWpf.General.mnuMainDashboard1  -   5          Name$ ucDashboard1��5E   0   5F      5G      5H      5I      � 5J   
   -   5        $ DevGrid1��5J      $��Stretch=�6"   
�������$��Bottom=�6T   $��20��6o   .+�5K      �� $��1��6   $	��Top=�6    
��� � �68   �� 5L      $ 
Horizontal=�6   .+�5M      �� $ 
Horizontal=�6   .+�5N      �� -   5        $ btnASIT��5N      $
��ASIT��6.   |�.    StdBtnStyle01#��[ 6O   $��80��6v   6�   5O      �� 5P      -   5         ��Name$ stkpComName��5P      $��30,5,0,0q�62   $ 
Horizontal=�6D   .+�5Q      �� $��12(�6    $��©��6.   $��Arial Unicode MS)�67   6W   �� 5R      $��11(�6    $J��D 2017-2020 Advanced Software & IT Services Ltd. All rights reserved.��6.   $��Arial Unicode MS)�6~   6�   5S      �� 5T      -   5        $ stkpContact��5T      $ 
Horizontal=�62   $��10,5,0,0q�6K   .+�5U      �� $��50��6"   .+�5V      �� $��11(�6$   
����  �$��	Contact: ��6D   $��Right=�6U   $��Arial Unicode MS)�6r   5W      �� 5X      $ 
Horizontal=�6!   $��5,0,0,0q�6:   .+�5Y      �� $
��Left=�6$   $��11(�6?   $��Arial Unicode MS)�6M   ><Phone: (+88)-02-9033408, 9116439, 9143472, 9118665, E-Mail: �� 5[      -   5        
����  ���.  $��mailto:info@asit.com.bd?�5[   q   .�6�   info@asit.com.bd6�   	, Web: �� 5\      -   5        
����  ���.  $��http://www.asit.com.bd?�5\   y   .�6�   www.asit.com.bd6�   5]      5^      5`      5a      ]� 5b      $��Right=�6   $	��350��65   .+�5c      �� -   5         ��Name$ slider1��5c      $	��180��6*   $
��Left=�5d       � Maximum$	 1.5Q�5e       � Minimum$	 0.5Q�5f      $��0,-5,0,0q�5g        �� TickFrequency$	  0.2Q�5h      ! �� TickPlacement$! TopLeft=�5i      |�.  " � Value" 5k      �� # �� ElementName$# UcGrid1��$ �� Path$$ LayoutTransform.ScaleX�% �� Mode$% OneWay=��� 5l      -   5        $ tbArrivalDateTime��5m      $��Right=�5l      $	��150��63   $��11(�6?   $��Right=�5n      $��0,2,0,0q�61   $	��Top=�5o      6,   (  "System.Windows.Controls.DatePicker  5p      -   5        &  Name$& dtpMW1��5p      $��Right=�6-   |�.  $y�Hidden=�6V   $��30��6j   $��12��6v   5q      5r      5s      5t      