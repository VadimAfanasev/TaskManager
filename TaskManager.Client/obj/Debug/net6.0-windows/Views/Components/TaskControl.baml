   M S B A M L   `   `   `  ����M  ITaskManager.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:TaskManager.Client.Views.Components#TaskManager.Client.Views.Components  5      5    /TaskManager.Client.Views.Components.TaskControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Clocal1clr-namespace:TaskManager.Client.Views.Components     5      .��5	      � .+�5
   
   ��    taskPanelBorder#��[  6   ._�5      �   � RowDefinitions  5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $��30 �6$   �� 5      $��30 �6$   �� 5      $��30 �6$   �� 5      $��30 �6$   .+�5      �� $
��Wrap=�6=     commonTBlock#��[ 5      ��5      ��  �� Path$ 
Model.Name�5      �� 5      $��1��6   $
��Wrap=�6Q   #��[ 5      ��5   )   �� $ Model.Description�5      �� 5      $��2��6    �� Orientation$ 
Horizontal=�6*   .+�5      �� $��	Creator: ��6    #��[ 5       �� 5      #��[ 5       ��5       �� $ Creator�5       5      �� 5!      $��3��6   $ 
Horizontal=�6*   .+�5"      �� $��
Executor: ��6    #��[ 5#       �� 5$      #��[ 5%       ��5$       �� $ Executor�5%       5&      �� 5(      $��4��6   $ 
Horizontal=�6*   .+�5)      �� $��Start: ��6    #��[ 5*       :  4System.Windows.Controls.Primitives.DatePickerTextBox  5+       �� 
IsReadOnly$
 True��6N   ��6(   �� $ Model.StartDate�6N   5,      �� 5.      $��5��6   $ 
Horizontal=�6*   .+�5/      �� $��End: ��6    #��[ 50         51      $
 True��6L   ��6(   �� $ Model.EndDate�6L   52      53      54      55      56      