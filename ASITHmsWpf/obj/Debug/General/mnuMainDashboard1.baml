   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ASITHmsWpf.MarketingASITHmsWpf.Marketing  5      *    $ASITHmsWpf.General.mnuMainDashboard1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .local"clr-namespace:ASITHmsWpf.Marketing  5      -   5        $��14(�5   I   .��5	      � .+�5
   
   �� 
������ .+�5      ��   �� Orientation$  
Horizontal=�6   $	��630��62   .+�5      �� ��5      �� 5       �� ImageSource$ ..\Images\bgOffWhite.png��6%    j� Stretch$
 Fill=�6L    �� Opacity$	 0.7Q�6[   .+�5      �� $  
Horizontal=�6!   $
��Left=�6:   $��22��6U   
������� J� Opacity$	 0.4Q�6t   .+�5      �� -   5         ��Name$ btnTvExpand��5   !   $��+��66   $��25��6B   |�.  $��0,2,0,2q�6_   $
��Bold"�6p   $��14(�5       
������ $��0q�6G    � ToolTip$ Expand all menu items��6u   �� 5      -   5        $ btnTvCollapsed��5   !   $��-��69   $��25��6E   |�.  $��3,2,0,2q�6c   $
��Bold"�6t   $��14(�5       
������ $��0q�6G   $ Collapse all menu items��6u   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5      $	��170��60   
������ $��3,2,0,2q�6U   $	��100��6f      	Watermark$ Type to find menu item��6u   $��Normal"�5   (   $��11(�6<   $��0q�6J   �� 5      -   5        $ 	btnTvFind��5   !   $��20��64   |�.  
������ $ Find menu item��6�   $��3,2,0,2q�6�   $��0q�6�   ��5      �� 5   "   $��..\Images\LookupGlass.png��6(   $
��Fill=�6K   5      5      R� 5       -   5         R�Name$ tvMenu1��5       $	��250��60   $��Hidden=�6<   $��Hidden=�6p   $	��580��5!      
������ $��1��6A   $��0q�6N   .@�5#      Q� $
��aaaa��6'   .@�5$      Q� $��a-xxxx��6+   Q� 5%      $��a-yyyy��6+   Q� 5&      $��a-zzzz��6+   5'      Q� 5(      $
��bbbb��6'   .@�5)      Q� $��b-xxxx��6+   Q� 5*      $��b-yyyy��6+   Q� 5+      $��b-zzzz��6+   5,      Q� 5-      $
��cccc��6'   .@�5.      Q� $��c-xxxx��6+   Q� 5/      $��c-yyyy��6+   Q� 50      $��c-zzzz��6+   51      Q� 52      $
��dddd��6'   .@�53      Q� $��d-xxxx��6+   Q� 54      $��d-yyyy��6+   Q� 55      $��d-zzzz��6+   56      57      58      �� 59      $	��630��6   $	��950��6*   $	��Top=�66   ��5:      �� 5;      $! ..\Images\dashbordpic_h.jpg��6%   $ UniformToFill=�6O   .+�5F      �� -   5        	 ��Name$	 	xctkGrapg��5F   !   $y�Visible=�64   .+�5G      �� $  
Horizontal=�6%   $
��Left=�6>   $��22��6Y   $	��950��6e   
�������$	 0.5Q�6�   .+�5H      �� $8��2Information Summary Dashboard (Upcoming Templates)��6(   
 �� TextAlignment$
 Right=�6b   $��Center=�6x    �� Padding$ 0,0,80,0q�6�   $	��680��6�   �� 5I      -   5         ��Name$ chkDashboardItems��5I   '    d� 	IsChecked$ FalseY�6B   $��Center=�6T   $#��Show Dashboard Template Items��6v   $	��270��6�   6�   5J      �� 5K      -   5         ��Name$ 
scrlvGrapg��5K   '   $y�	Collapsed=�6<   $
��Auto=�6T   $	��630��6w   $	��950��6�   $��0,5,0,0q�6�   .��5L      � .+�5N   "   )   #ASITHmsWpf.Marketing.frmDashboardG1  )   #ASITHmsWpf.Marketing.frmDashboardG1   MouseDoubleClick$% frmDashboardG1_MouseDoubleClick��6o   $��Stretch=�67   $��0,0,0,0q�6U   
 � Tag$ G1��6f   6o   )   #ASITHmsWpf.Marketing.frmDashboardG2  5O   "   )   #ASITHmsWpf.Marketing.frmDashboardG2   MouseDoubleClick$% frmDashboardG1_MouseDoubleClick��6q   $��Stretch=�67   $��	10,0,10,0q�6U   $ G2��6h   6q   )   #ASITHmsWpf.Marketing.frmDashboardG3  5P   "   )   #ASITHmsWpf.Marketing.frmDashboardG3   MouseDoubleClick$% frmDashboardG1_MouseDoubleClick��6o   $��Stretch=�67   $��0,0,0,0q�6U   $ G3��6f   6o   )   #ASITHmsWpf.Marketing.frmDashboardG4  5Q   "   )	   #ASITHmsWpf.Marketing.frmDashboardG4 	  MouseDoubleClick$% frmDashboardG1_MouseDoubleClick��6p   $��Stretch=�67   $��0,10,0,0q�6U   $ G4��6g   6p   )
   #ASITHmsWpf.Marketing.frmDashboardG5
  5R   "   )   #ASITHmsWpf.Marketing.frmDashboardG5   MouseDoubleClick$% frmDashboardG1_MouseDoubleClick��6r   $��Stretch=�67   $��
10,10,10,0q�6U   $ G5��6i   6r   )   #ASITHmsWpf.Marketing.frmDashboardG6  5S   "   )   #ASITHmsWpf.Marketing.frmDashboardG6   MouseDoubleClick$% frmDashboardG1_MouseDoubleClick��6p   $��Stretch=�67   $��0,10,0,0q�6U   $ G6��6g   6p     5U   "   $% frmDashboardG1_MouseDoubleClick��6p   $��Stretch=�67   $��0,10,0,0q�6U   $ G7��6g   6p     5V   "   $% frmDashboardG1_MouseDoubleClick��6r   $��Stretch=�67   $��
10,10,10,0q�6U   $ G8��6i   6r     5W   "   $% frmDashboardG1_MouseDoubleClick��6p   $��Stretch=�67   $��0,10,0,0q�6U   $ G9��6g   6p     5X   "   $% frmDashboardG1_MouseDoubleClick��6q   $��Stretch=�67   $��0,10,0,0q�6U   $	 G10��6g   6q   
  5Y   "   $% frmDashboardG1_MouseDoubleClick��6s   $��Stretch=�67   $��
10,10,10,0q�6U   $	 G11��6i   6s     5Z   "   $% frmDashboardG1_MouseDoubleClick��6q   $��Stretch=�67   $��0,10,0,0q�6U   $	 G12��6g   6q   5[      5\      5]      5^      5_      5`      5a      5b      