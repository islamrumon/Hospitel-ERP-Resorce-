   M S B A M L   `   `   `  ����   
ASITHmsWpf-    'ASITHmsWpf.Commercial.frmEntryDocVisit1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -   
  � Tag$2  ,Patient Visit Token Information Input Screen��   IOUserControl#��[  .��� .+���  �� Orientation$ Vertical=�.+��� $
��Left=�$ 
Horizontal=�.+��� $�� Patient Token :��$��14(�$��Italic$�$
��Bold"�
����� �$	��120���� -    ��Name$ lblSectCodTitle��$��Loca_tion :��$��Right=�$��60�� �� Target ��  �� ElementName$ 
cmbSectCod���� -    ��Name$ 
cmbSectCod��$	��200�� �� 
IsReadOnly .  �� SelectedIndex$ 0��|�.  �� -   $ lblPrevTokenListTitle��$��Prev.Token List :��$��Right=�$	��110�� �� $ cmbPrevTokenList���� -   $ cmbPrevTokenList��$	��450�� . $ 0��|�.  �� $��15��$y�Hidden=��� -    ��Name$ 	btnPrint1��$��_View/Print��$��75��$��22��  StdBtnStyle01#��[ �� $��20��$y�Hidden=��� -   	 ��Name$	 ChkPrintDirect��$��Direct Print��
 d� 	IsChecked$

 TrueY�$��Center=��� $��45��$y�Hidden=��� $ 
Horizontal=�.+��� -    ��Name$ stkIntro��$ Vertical=�$	��Top=�$
��Left=�$��120,3,3.6,0q�.+��� $ 
Horizontal=�.+��� -	   $ lblDocTitle��$��Doctor :��$��Right=�$��60�� �� $ 
cmbSectCod���� -
   $ cmbDoc��$	��200�� . $ 0��|�.  �� $��30��$y�Hidden=��� -   $	 chkDateBlocked��$��Visit _Date : ��|�.  $��Right=�$��Center=�$��80��&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtVisitDat��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��130��{�.  �� -   $ lblTokenNoTitle��$��	Token No:��$��Right=�$��80���� -   $ 
lblTokenNo��$��	DTI-DDXXX��$  DTIYYYYMMCCCCDDXXX��$	��140��$
��Bold"�$
��Left=��� $��20��$y�Hidden=��� $ 
Horizontal=�$��24��.+��� -   $ 	btnPrint2��$��_Print��$��75��#��[ $��24��$	��Top=��� $��16��$y�Hidden=��� -   $ btnOk��#��[ $	��_Ok��$��75���� $��20��$y�Hidden=��� -   $	 ChkPrintTokenList��$��Print Token List��$
 FalseY�$��Center=��� $	��Top=�
��� ���$��2��$��0,60,0,0q��� -   $ stkpDetails��$ 
Horizontal=�$��0,70,0,0q�.+��� $	��380��.+��� $	��300��$	��Top=�$	��120�����	#FFF3F1F1$
��Left=�$��10,0,0,0q�.+��� $��TOKEN��$��Center=�$��30(�$
��Bold"��� -   $ lblLastTokenNo��$��00��$��Center=�$��40(�$
��Bold"��� $
��Left=�$��
10,150,0,0q�.+�&   System.Windows.Controls.DataGrid  -     Name$ 	dgvDocFee��   CanUserResizeRows .  
������    
IsReadOnly .  $��0q�   CanUserResizeColumns .  $	��Top=�$	��200��$	��300��$
��Auto=�   AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  $��Hidden=�   	RowHeight$ 25Q�   RowHeaderWidth$ 10Q�   SelectionUnit$
 Cell=�   SelectionMode$ Single=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 200    CanUserSort .     
IsReadOnly .    CanUserResize .  1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ visitdes�   HeaderTemplate 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $��Center=�$	��Top=�.+��� $��Center=�$	��Top=�$��Description��|�.  $	��100��4  .System.Windows.Controls.DataGridTemplateColumn  $ 90     Header$  Doctor's Fee�� .  !   CellTemplate! �� .R�(	  "Xceed.Wpf.Toolkit.WatermarkTextBox	  -   " �� TextAlignment$" Right=�)
  #Xceed.Wpf.Toolkit.AutoSelectTextBox# 
  AutoMoveFocus# . $��0q�$��80��$��Right=�$ 
  AutoSelectBehavior$$ OnFocus=�$��5,0,0,0q� 
 wtxtBx#��[ % 	  	Watermark$% Amount������ $ docfee�& �� Mode$& TwoWay=�' �� UpdateSourceTrigger$' PropertyChanged=��� .+��� -   $ stkpDetails1��.+��� $ 
Horizontal=�.+��� -   $	 chkPatTrnID��$��C_ust ID : ��|�.  $��Right=�$��Center=�$��80��$��30,0,0,0q�	  -   ( 	 Name$( txtPatID��#��[ $   ��# . {�.  $	��130��$$ OnFocus=��� $ 
Horizontal=�$��0,5,0,0q�.+��� -   $ 
lblPatName��$��P_atient Name :��$	��110��$��Right=� �� $ 
txtPatName��	  -   $( 
txtPatName��#��[ $   ��# . $	��370��$$ OnFocus=��� -   $ lblPatAgeTitle��$��A_ge :��$��Right=�$��50�� �� $ 	txtPatAge��	  -   $( 	txtPatAge��$" Right=�# . $$ OnFocus=�$��5,0,5,0q�#��[ $�� ��$��70���� ) �� TextAlignment$
) Left=�* �� Padding$* 5,0,0,0q�
�������$��Years��$��50���� $
��Left=�.+��� $ 
Horizontal=�$��0,5,0,0q�.+��� $ Vertical=�.+��� $ 
Horizontal=�$	��330��.+��� -   $ lblVisitNumTitle1��$��Visit No. :��$��Right=�$	��110�� �� $ 
cmbVisitNo���� -   $ 
cmbVisitNo��$	��180��$	��Top=�$
��Bold"�$ 0���� $ Vertical=�.+��� $ 
Horizontal=�.+��� -   $ lblDocFeeTitle1��$!��_Doctor's Visiting Charge :��$��Right=�$	��200�� �� $ 	txtDocFee��	  -    $( 	txtDocFee��$" Right=�# . $$ OnFocus=�$��5,0,5,0q�#��[ $% Doctor's Fee��$�� ��$	��120���� $ 
Horizontal=�$��0,2,0,0q�.+��� -!   $ lblOthrFeeTitle1��$��Others Charge (if any) :��$��Right=�$	��200�� �� $ txtOthrChrg��	  -"   $( txtOthrChrg��$" Right=�# . $$ OnFocus=�$��5,0,5,0q�#��[ $% Other Charge��$�� ��$	��120���� $ 
Horizontal=�$��0,2,0,0q�.+��� -#   $ lblDiscAmtTitle1��$��Discount Amount :��$��Right=�$	��200�� �� $ 
txtDiscAmt��	  -$   $( 
txtDiscAmt��$" Right=�# . $$ OnFocus=�$��5,0,5,0q�#��[ $% Discount��$�� ��$	��120���� $ 
Horizontal=�$��0,2,0,0q�.+��� -%   $ lblTotalTitle1��$��Total Payable Amount :��$��Right=�$	��200��$
��Bold"��� -&   + ��Name$+ txtNetTotal��$) Right=�$
��Bold"�$	��120��$* 5,0,5,0q�
��������� $ 
Horizontal=�$��0,5,0,0q�.+��� -'   $ lblOthrDesc��$��N_otes (if any):��$	��110��$��Right=� �� $ txtOthrDesc��	  -(   $( txtOthrDesc��#��[ , �� TextWrapping$
, Wrap=�$   ��$��40��$	��540��$$ OnFocus=��� $ 
Horizontal=�$��Right=�$��0,5,0,0q�.+��� -)   $	 ChkConfirmSave��|�.  $��Need Confirmaation��$
 FalseY�$��Center=�$��	0,0,100,0q��� -*   $ 	btnUpdate��#��[ $��_Save��$��75��$	��Top=�$��24���� $��0,20,0,0q�.+�  -+   $ dgvVisitList�� .  
������  .  $��0q�$��0,0,0,0q�$��0��$	��Top=�$	��200��$
��Auto=� .   .   .  $��Hidden=�$ FullRow=�$ Single=�-   FrozenColumnCount$- 2��$ 25Q�   $ 40 $  Token��. +� IsFocusScope. .   .   . /   CanUserReorder/ .   .   �� $ slnum�0 �� StringFormat$0 {0: 00}��  $	 290  .   .  .   �� $ ptname�   .R�� $��Center=�$	��Top=�.+��� $��Center=�$	��Top=�$��Description��|�.  $	��180��  $	 180 $  
Visit Desc��. .   .   . / .   .   �� $ visitnum�  $	 110 $  Amount�� .   . / .   .  ! �� .R�� $��Right=�$	��Top=�$	��100��.+��� $��Right=�$	��Top=�$* 5,0,5,0q����� *Netam+$0 {0: #,##0.00}���� $ 
Horizontal=�$��Right=�.+��� -,   $ lblTota1Title��$��Total Amount :��$��Right=�$	��200��$
��Bold"�$��14(��� --   $ lblTota1��$�� ��$��Right=�$	��120��$
��Bold"�$��0,0,25,0q�$��14(�