   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  'clr-namespace:ASITHmsWpf
ASITHmsWpf  /    )ASITHmsWpf.Commercial.frmReportPharmaPOS1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  $localclr-namespace:ASITHmsWpf  -   
  � Tag$.  (Medicine Sales Reports View/Print Screen��   IOUserControl#��[  .���  � RowDefinitions �� $	��95* ��� $
��500* � � ColumnDefinitions �� $	��77* ��� $
��323* �.+�R� -    R�Name$ 
tvRptTitle��$��0��
������ $��1����  �� Orientation$ Vertical=�$	��Top=�$	��280��$��0,10,0,0q�$��0��$��0��$��3��$��2��.+��� $ 
Horizontal=�.+��� $��Dated From :��$��90��$��Right=�&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtpFrom��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��130���� $��Main Location :��$	��100��$��Right=��� -    ��Name$ 
cmbSectCod�� �� SelectedIndex$ 0��$	��230���� -    ��Name$ lblStaffIdTitle��$��Staff :��$��Right=�$��60��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -   	  Name$	 AtxtStaffId��
   
SearchType$
 C��$
��Left=�$��22��$	��330���� $��Output Options :��$	��100��$��Right=��� -   $ cmbOutputOption��$	��150��$
��Bold"�$ 3��.@��� $��Print preview��$  PP���� $��Save as PDF file��$	  PDF���� $��Print directly to printer��$  DP���� $��Show in screen��$  SS���� $!��Export to Excel with format��$  EXCELF���� $!��Export to Excel - Data only��$  EXCEL���� $ ��Export to Word with format��$
  WORD���� $ 
Horizontal=�$��0,5,0,0q�.+��� $��As On /��$��Center=��� $
��To :��$��30��$��Right=�  -   $ 
xctk_dtpTo��#��[ $
��Bold"�$��22��$	��130���� $��Other Location :��$	��100��$��Right=��� -	   $ cmbSectCod2��$ 0��$	��230���� -
   $ lblSupIdTitle��$��Client :��$��Right=�$��60��  -   $	 	AtxtSupId��$
 C��$
��Left=�$��22��$	��330���� $ 
Horizontal=�$��0,5,0,0q�.+��� $��Select Shop :��$��90��$��Right=��� -   $ cmbShop��$ 0��$	��130��.@��� $��Medicine Shop��$	  MSI���� $��
Restaurent��$	  FSI���� $��Item Group :��$	��100��$��Right=��� -   $ cmbItemGroup��$ 0��$	��230���� -   $ lblItemIdTitle��$��Item :��$��Right=�$��60��  -   $	 
AtxtItemId��$
 C��$
��Left=�$��22��$	��330���� $��10��$y�Hidden=��� -    ��Name$ btnGenerate��  StdBtnStyle01#��[ $��	_Generate��$��75���� $��1��$��1��$��2��.+��� $��0q�
��� ����� $��45��$��Center=�$	��950��.+��� -   $ lbltle1�����	#FF862222$��Center=�$
��Bold"�$��Verdana)��� -   $ lbltle2�����	#FF862222$��Center=�$
��Bold"�$��Verdana)��� .+�&   System.Windows.Controls.DataGrid  -     Name$ dgOverall01��   AlternatingRowBackground �	#FFE4F2E1   CanUserSortColumns .     
IsReadOnly . $	��950��$��Center=�$	��450��$��Visible=�$��10,0,9,0q� �� 
GroupStyle (  "System.Windows.Controls.GroupStyle     ContainerStyle ��  �� 
TargetType" ��.���  �� Property � �� �� Value ��  �� 
TargetType" ��.R�9�  9� 
IsExpanded .  
�������$��1q����� .+��� $
��Bold"�
����  �$��12(� �� TextAlignment$ Center=����� *Name+.���� 