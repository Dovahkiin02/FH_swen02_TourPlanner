   M S B A M L   `   `   `  ����H  DTourPlannerUi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:TourPlannerUi.ViewsTourPlannerUi.Views  5      ;"clr-namespace:TourPlannerUi.ModelsTourPlannerUi.Models  5      /    )TourPlannerUi.Views.CreateAndEditTourView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:TourPlannerUi.Views     5      ] YXceed.Wpf.Toolkit.NET5, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  6>   -enum"clr-namespace:TourPlannerUi.Models  5      .��5
      �   � ColumnDefinitions  5   
   �� 5      $	��200 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6    $��ID��6-   �� 5   
   $��1��6   $��0��6"   ${�False��6H   ��6/   �� *	Tour.Id+6H   �� 5   
   $��0��6   $��1��6    $
��Name��6-   �� 5   
   $��1��6   $��1��6"   ��6/   �� *	Tour.Name+�� 5!   
   $��0��6   $��2��6    $��Description��6-   �� 5"   
   $��1��6   $��2��6"   ��6/   �� *Tour.Description+�� 5$   
   $��0��6   $��3��6    $
��From��6-   �� 5%   
   $��1��6   $��3��6"   ��6/   �� *	Tour.From+�� 5'   
   $��0��6   $��4��6    $��To��6-   �� 5(   
   $��1��6   $��4��6"   ��6/   �� *	Tour.To+�� 5*   
   $��0��6   $��5��6    $��Transport Type��6-   �� 5+   
   $��1��6   $��5��6#    �� SelectedItem 60   �� *Tour.TransportType+��5,      �� *TransportTypes+�� 5.   
   $��6��6   $��1��6"   $
��Left=�62    �� Orientation$ 
Horizontal=�6M   $��0 2 0 0q�6f   .+�5/      �� $��0��6   $
��Save��6%   ��64   �� *SaveCommand+�� 50      $��1��6   $��Cancel��6%   $��15 0 0 0q�6X   ��66   �� *CancelCommand+6X   52      53      54      