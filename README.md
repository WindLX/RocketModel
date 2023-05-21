### RocketModel

Unity 搭建的 RocketPlus 地面站数据实时绑定回放软件。

与地面站主体部分采用 Socket 通讯，地面站主体程序以 200ms 间隔向 RocketModel 传输一次数据，RocketModel 每 800ms 更新一次数据。界面 UI 部分交互设计采用 Mvvm 模式，数据传输均使用事件总线进行，火箭尾焰效果使用 ShaderGraph 制作。

