/// <summary>
/// 枚举类型
/// </summary>
public class EnumType
{
    /// <summary>
    /// UI 界面
    /// </summary>
    public enum UIPanel
    {
        // 主界面, 设置界面, 添加事项界面, 修改事项界面, 个人界面, 修改信息界面, 关于app界面
        PnlMain, PnlSettings, PnlNewItem, PnlModifyItem, PnlSelf, PnlModifyInfo, PnlAbout
    }

    /// <summary>
    /// 颜色主题
    /// </summary>
    public enum ColorTheme
    {
        // 白色, 粉色, 蓝色, 黑色
        White, Pink, Blue, Dark
    }
}
