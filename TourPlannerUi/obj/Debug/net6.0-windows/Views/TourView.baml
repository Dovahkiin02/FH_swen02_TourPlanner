   M S B A M L   `   `   `  ����H  DTourPlannerUi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:TourPlannerUi.ViewsTourPlannerUi.Views  5      g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137akOclr-namespace:MahApps.Metro.IconPacks;assembly=MahApps.Metro.IconPacks.MaterialMahApps.Metro.IconPacks 6>   C&clr-namespace:TourPlannerUi.ConvertersTourPlannerUi.Converters  5      "    TourPlannerUi.Views.TourView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:TourPlannerUi.Views     5      e	iconPacksOclr-namespace:MahApps.Metro.IconPacks;assembly=MahApps.Metro.IconPacks.Material     6>   =
converters&clr-namespace:TourPlannerUi.Converters     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5        � 	Resources  5      6   0TourPlannerUi.Converters.BooleanToYesNoConverter   BoolToYesNoConverter��%   &	          5   
   6-   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   .@�5      �� $��	Tour Info��6   .��5      �� .��5      �� $��20q�6!   .+�5      ��  �� Orientation$ 
Horizontal=�6%   .+�5      �� $
��ID: ��6$   $
��Bold"�63   �� 5      $��Center=�6I   ��6(   �� *SelectedTour.Id+6I   5      �� 5!      $ 
Horizontal=�6%   .+�5"      �� $��Name: ��6$   $
��Bold"�65   �� 5#      $��Center=�6K   ��6(   �� *SelectedTour.Name+6K   5$      �� 5&      $ 
Horizontal=�6%   .+�5'      �� $��Description: ��6$   $
��Bold"�6<   �� 5(      $��Center=�6R   ��6(   �� *SelectedTour.Description+6R   5)      �� 5+      $ 
Horizontal=�6%   .+�5,      �� $��From: ��6$   $
��Bold"�65   �� 5-      $��Center=�6K   ��6(   �� *SelectedTour.From+6K   5.      �� 50      $ 
Horizontal=�6%   .+�51      �� $
��To: ��6$   $
��Bold"�63   �� 52      $��Center=�6I   ��6(   �� *SelectedTour.To+6I   53      �� 55      $ 
Horizontal=�6%   .+�56      �� $��Transport Type: ��6$   $
��Bold"�6?   �� 57      $��Center=�6T   ��6(   �� *SelectedTour.TransportType+6T   58      �� 5:      $ 
Horizontal=�6%   .+�5;      �� $��
Distance: ��6$   $
��Bold"�69   �� 5<      $��Center=�6O   ��6(   �� *SelectedTour.Distance+6O   5=      �� 5?      $ 
Horizontal=�6%   .+�5@      �� $��Estimated Time: ��6$   $
��Bold"�6?   �� 5A      $��Center=�6N   ��6(   �� *SelectedTour.EstTime+6N   5B      �� 5D      $ 
Horizontal=�6%   .+�5E      �� $��Popularity: ��6$   $
��Bold"�6;   �� 5F      $��Center=�6Q   ��6(   �� *SelectedTour.Popularity+6Q   5G      �� 5I      $ 
Horizontal=�6%   .+�5J      �� $��Child Friendly: ��6$   $
��Bold"�6?   �� 5K      $��Center=�6T   ��6(   �� *SelectedTour.ChildFriendly+6T   5L      5M      5N      5O      �� 5P      $��Tour Map��6   .��5Q      �� ��6   �� *	TourImage+5R      5S      &   System.Windows.Controls.DataGrid  5V   
   -   5          Name$ TourLogDataGrid��5V       $��1��6      AutoGenerateColumns$ False��6Z      CanUserAddRows$ False��6v      CanUserDeleteRows$ False��6�      
IsReadOnly$
 True��6�   ��69   �� *
TourLogs+   Columns 5W      0  *System.Windows.Controls.DataGridTextColumn  5X      ,  &System.Windows.Controls.DataGridColumn	   Header$	 	Date/Time��6%   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 68   �� *Date+  5Y      $	 
Difficulty��6%   
 69   �� *
Difficulty+  5Z      $	 Duration��6%   
 67   �� *
Duration+  5[      $	 Rating��6%   
 65   �� *Rating+  5\      $	 Comment��6%   
 66   �� *	Comment+4  .System.Windows.Controls.DataGridTemplateColumn  5]      $	 Actions��6)      CellTemplate 5^      �� 5_      .R�5`      �� $ 
Horizontal=�6)   .+�5a   "   &    TourPlannerUi.Views.CustomButton     ToolTip$ Edit Tour Log��6�      Command 65   �� *DataContext.EditCommand+ �� RelativeSource ��  �� AncestorType DataGrid   CommandParameter 6�   ��    Content. 5b   &   .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$ Pencil=�6A   $��Center=�6O   5c   #     5d   "   $ Delete Tour Log��6�   $��
5, 0, 0, 0q�6�    65   �� *DataContext.DeleteCommand+ ��  DataGrid 6�   �� . 5e   &     $ Delete=�6A   $��Center=�6O   5f   #   5g      5h      5j      5l      �� 5m   
   $��2��6   $
��Left=�6#   $ 
Horizontal=�6>   .+�5n      �� $
��Left=�68   $��
0, 5, 0, 0q�6S   ��6   �� *CreateCommand+.��5o      �� $ 
Horizontal=�6   .+�5p        $
 Plus=�61   $��Center=�6=   �� 5q      $��New Tour Log��6    $��Center=�64   $��
5, 0, 0, 0q�6O   5r      5s      �� 5u      $
��Left=�6<   $��
5, 5, 0, 0q�6W   ��6   �� *GeneratePdfCommand+.��5v      �� $ 
Horizontal=�6   .+�5w        $ FileDocumentOutline=�61   $��Center=�6L   �� 5x      $��Tour Report��6    $��Center=�63   $��
5, 0, 0, 0q�6N   5y      5z      5{      5|      5}      