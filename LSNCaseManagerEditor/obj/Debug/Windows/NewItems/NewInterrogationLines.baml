   M S B A M L   `   `   `  ����T  PLSNCaseManagerEditor, Version=12.25.2018.1, Culture=neutral, PublicKeyToken=nullK*clr-namespace:LSNCaseManagerEditor.WindowsLSNCaseManagerEditor.Windows  5   	   8    2LSNCaseManagerEditor.Windows.NewInterrogationLines   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:LSNCaseManagerEditor.Windows     5   	   -   5          � Title$  NewInterrogationLines��5   	   $	��450��6'   $	��650��64    � WindowStartupLocation$ CenterScreen=�6Y   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��25 �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6    $��5,5,5,5q�6-   $��	Question:��6>   $��Right=�6R   �� 5   
   $��0��6   $��1��6    $��5,5,5,5q�6-   $��Answer:��6>   $��Right=�6P   �� 5   
   $��0��6   $��2��6    $��5,5,5,5q�6-   $��Correct Answer:��6>   $��Right=�6X   �� 5   
   $��0��6   $��3��6    $��5,5,5,5q�6-   $��Response Truth (Player):��6>   $��Right=�6a   �� 5   
   $��0��6   $��4��6    $��5,5,5,5q�6-   $"��Response Truth (Interrogee):��6>   $��Right=�6e   �� 5    
   $��0��6   $��5��6    $��5,5,5,5q�6-   $��Response Doubt (Player):��6>   $��Right=�6a   �� 5!   
   $��0��6   $��6��6    $��5,5,5,5q�6-   $"��Response Doubt (Interrogee):��6>   $��Right=�6e   �� 5"   
   $��0��6   $��7��6    $��5,5,5,5q�6-   $��Response Lie (Player):��6>   $��Right=�6_   �� 5#   
   $��0��6   $��8��6    $��5,5,5,5q�6-   $ ��Response Lie (Interrogee):��6>   $��Right=�6c   �� 5%   
   -   5         ��Name$ Question��5%   @   $��1��6   $��0��6"   $��5,5,5,5q�6/    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�6g   }�.  �� 	IsEnabled . 6�   �� 5&   
   -   5        $ Answer��5&   @   $��1��6   $��1��6"   $��5,5,5,5q�6/    . $
 Auto=�6e   }�.  . 6�   �� 5'   
   -   5         ��Name$ CorrectAnswer��5'   A   $��1��6   $��2��6#   $��5,5,5,5q�60   	 �� SelectedIndex$	 0��6X   .@�5(      �� .��6   Truth6"   �� 5)      .��6   Doubt6"   �� 5*      .��6   Lie6    5+      �� 5,   
   -   5        $ PlayerTruth��5,   @   $��1��6   $��3��6"   $��5,5,5,5q�6/    . $
 Auto=�6j   }�.  . 6�   �� 5-   
   -   5        $ InterrogeeTruth��5-   @   $��1��6   $��4��6"   $��5,5,5,5q�6/    . $
 Auto=�6n   }�.  . 6�   �� 5.   
   -   5        $ PlayerDoubt��5.   @   $��1��6   $��5��6"   $��5,5,5,5q�6/    . $
 Auto=�6j   }�.  . 6�   �� 5/   
   -   5        $ InterrogeeDoubt��5/   @   $��1��6   $��6��6"   $��5,5,5,5q�6/    . $
 Auto=�6n   }�.  . 6�   �� 50   
   -	   5        $ 	PlayerLie��50   @   $��1��6   $��7��6"   $��5,5,5,5q�6/    . $
 Auto=�6h   }�.  . 6�   �� 51   
   -
   5        $ InterrogeeLie��51   @   $��1��6   $��8��6"   $��5,5,5,5q�6/    . $
 Auto=�6l   }�.  . 6�   �� 53   
   -   5        $��0��53   *   $��2��67   $��2��6G   $��Center=�6[   $��Center=�6x   $��20��6�   $��20��6�   
�������$��0q�6�   .��54      �� $��../gfx/main/item_info.png��6   
 � ToolTip$4
 .Press me if you need any help filling this out��67   $��UniformToFill=�6p   55      56      57      