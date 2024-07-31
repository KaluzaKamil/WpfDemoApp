# WpfDemoApp

Demo apllication using WPF to render a few basic shapes specified in the selected json file.
Application automaticaly scales down the view to fit all currently rendered shapes in the window.

Json file can have different shapes specified in it with ARGB color schema.
Currently implemented shapes are line, circle and triangle.

Example json utilizing all shapes
```json
[ 
	{ 
		"type": "line", 
		"a": "-1,5; 3,4", 
		"b": "2,2; 5,7", 
		"color": "127; 255; 255; 255" 
	}, 
	{ 
		"type": "circle", 
		"center": "0; 0", 
		"radius": 15.0, 
		"filled": false, 
		"color": "127; 255; 0; 255" 
	}, 
	{ 
		"type": "triangle", 
		"a": "-15; -20", 
		"b": "15; -20,3", 
		"c": "0; 21", 
		"filled": true, 
		"color": "127; 0; 255; 255" 
	}
]
```
