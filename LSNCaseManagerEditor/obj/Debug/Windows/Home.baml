   M S B A M L   `   `   `  ����T  PLSNCaseManagerEditor, Version=12.25.2018.1, Culture=neutral, PublicKeyToken=null;"clr-namespace:LSNCaseManagerEditorLSNCaseManagerEditor  5   	   %    LSNCaseManagerEditor.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:LSNCaseManagerEditor     5   	   -   5          � Title$  L.S. Noir Case Manager��5   	   $	��600��6(   $	��600��65    � WindowStartupLocation$ CenterScreen=�6A   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   .+�5   
   `� $��0��6   $
��Left=�6#   $	��Top=�6>   $	��592��6V   .�5      c�  c� Band$ 0��6    c� 	BandIndex$ 0��6   .@�5      �� -   5        $��ToolBarHome��5      .��5      �� $ ��../gfx/toolbar/tb_home.png��6    � ToolTip$ Return to home��6@   5      �� 5      -   5        $��ToolBarNewCase��5      .��5      �� $��../gfx/toolbar/tb_new.png��6   $ Create new case��6?   5      �� 5      -   5        $��ToolBarOpenCase��5      .��5      �� $ ��../gfx/toolbar/tb_open.png��6   $ Open existing case��6@   5      �� 5      -   5        $��ToolBarSaveCase��5      .��5      �� $ ��../gfx/toolbar/tb_save.png��6   $ Save current case��6@   5       5!      c� 5"      $ 0��6   $ 1��6   .@�5#      �� -   5        $��ToolBarAddItem��5#      .��5$      �� $#��../gfx/toolbar/tb_newItem.png��6   $ Add case item��6C   5%      �� 5&      -   5        $��ToolBarEditItem��5&      .��5'      �� $$��../gfx/toolbar/tb_itemEdit.png��6   $ Edit selected item��6D   5(      �� 5)      -   5        $��ToolBarRemoveItem��5)      .��5*      �� $#��../gfx/toolbar/tb_delItem.png��6   $ Remove case item��6C   5+      5,      c� 5-      $ 0��6   $ 2��6   $��Right=�6-   .@�5.      �� -	   5        $��ToolBarHelp��5.      .��5/      �� $ ��../gfx/toolbar/tb_help.png��6   $ Case information��6@   50      51      52      �� 54   
   -
   5        $��MainWindowDisplay��54   0   $��1��6   $��0��6    $��Stretch=�6I   $��10,10,10,10q�6g   $	��500��6|    �� View 55      �� 56       �� AllowsColumnReorder . .P�57      �� $��ID��6%   	 �� Width$		 175��6U   
 �� DisplayMemberBinding
 61   �� *ID+6U   �� 58      $��	Data Type��6%   $		 150��6b   
 68   �� *
DataType+6b   �� 59      $
��Name��6%   $		 175��6Y   
 63   �� *Name+6Y   5:      5<      �� 5>   
   $��3��6   $
��Left=�6!   $��30��6<   $��Bottom=�6H   $	��592��6c   .@�5?      �� $��Current Case:��6   $��10(�6,   �� 5@      -   5        $��CurrentCaseLbl��5@      $
��None��6*   $��10(�69   $	��175��6G   $��DemiBold"�6S   �� 5A      -   5         ��Name$ MemUsed��5A      $��10(�6%   $��Stretch=�63   $��MEMORY USED��6Q   �� 5B      $��Last Saved:��6   $��10(�6*   $��Right=�68   �� 5C      -   5        $��LastSavedLbl��5C      $��10(�6(   $
��Left=�66   $��DemiBold"�6Q   5D      5E      5F      