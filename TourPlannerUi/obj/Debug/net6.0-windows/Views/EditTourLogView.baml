   M S B A M L   `   `   `  ����H  DTourPlannerUi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:TourPlannerUi.ViewsTourPlannerUi.Views  5      C&clr-namespace:TourPlannerUi.ConvertersTourPlannerUi.Converters  6>   )    #TourPlannerUi.Views.EditTourLogView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:TourPlannerUi.Views     5      =
converters&clr-namespace:TourPlannerUi.Converters     6>     � 	Resources  5	      8   2TourPlannerUi.Converters.StringToTimeSpanConverter   StringToTimeSpanConverter8   2TourPlannerUi.Converters.NullToVisibilityConverter  NullToVisibilityConverter��%:   &	        &	        5
   
   6/     5   
   6/   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6    $��ID��6-   �� 5    
   $��1��6   $��0��6"   ${�False��6K   ��6/   �� *
TourLog.Id+6K   �� 5"   
   $��0��6   $��1��6    $
��Date��6-   (  "System.Windows.Controls.DatePicker  5#   
   $��1��6   $��1��6%      SelectedDate 62   �� *TourLog.Date+�� 5%   
   $��0��6   $��2��6    $��
Difficulty��6-   �� 5&   
   $��1��6   $��2��6#    �� SelectedItem 60   �� *TourLog.Difficulty+��5'      �� *Difficulties+�� 5)   
   $��0��6   $��3��6    $��Duration��6-   �� 5*   
   $��1��6   $��3��6"   ��6/   �� *TourLog.Duration+ �� 	Converter# [  �� 5,   
   $��0��6   $��4��6    $��Rating��6-   �� 5-   
   $��1��6   $��4��6#    60   �� *TourLog.Rating+��5.      �� *	Ratings+�� 50   
   $��0��6   $��5��6    $��Comment��6-   �� 51   
   $��1��6   $��5��6"   ��6/   �� *TourLog.Comment+�� 53   
   $��0��6   $��6��6    $��Tour ID��6-   �� 54   
   $��1��6   $��6��6"   ${�False��6O   ��6/   �� *TourLog.TourId+6O   �� 56   
   $��7��6   $��1��6"   $
��Left=�62    �� Orientation$ 
Horizontal=�6M   $��0 2 0 0q�6f   .+�57      �� $��0��6   $
��Save��6%   ��64   �� *SaveCommand+�� 58      $��1��6   $��Cancel��6%   $��15 0 0 0q�6X   ��66   �� *CancelCommand+6X   59      5:      5;      