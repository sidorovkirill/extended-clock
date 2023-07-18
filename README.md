# Unity Extended Clock
Simple SDK with implementation of clock, stopwatch and timer. Solution based on Zenject and UniRx.

## Concerns for UI in iOS platform.
My main concern is the difference in the front camera panel on different iPhone models. We need to ensure that the top panel with tabs is not hidden under the front camera panel. Also, make sense to adjust ui elements according to iOS style guides.

## After release proposals
1) Separate the state machine from ToolBar and use some third-party solution like Stateless.
2) Adjust UI flexibility to easiest build under all iOS devices (iPhone and iPad). Maybe would be easier to use an advanced Ui library like Nova to achieve it .
3) Make a scene root object similar to the MainCanvas object, to collect all scene objects in one parent and simplify integration of scene views to the DI conrainer.