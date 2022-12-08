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
        // 主界面, 主页界面, 商店界面, 个人界面, 设置界面, 添加事项界面, 修改事项界面, 修改信息界面, 关于app界面
        PnlMain, PnlHome, PnlShop, PnlSelf, PnlSettings, PnlNewItem, PnlModifyItem, PnlModifyInfo, PnlAbout
    }

    /// <summary>
    /// 颜色主题
    /// </summary>
    public enum ColorTheme
    {
        // 素雅灰, 胭脂粉, 清新蓝, 抹茶绿
        Grey, Pink, Blue, Green
    }
}
