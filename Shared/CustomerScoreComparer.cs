namespace Yzh.Bosai.Net.ScoreManager.Shared
{
    /// <summary>
    /// 对比排序
    /// </summary>
    public class CustomerScoreComparer : IComparer<CustomerScore>
    {
        /// <summary>
        /// 比较两个CustomerScore对象
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(CustomerScore? x, CustomerScore? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            //  x  CompareTo y
            //  Sort:【-1】 【0】 【1】
            // <0 : x < y
            // =0 : x = y
            // >0 : x > y

            // 反向比较，从高到低排序
            var scoreComparison = y.Score.CompareTo(x.Score);

            // y  CompareTo x
            // <0 : y < x
            // =0 : y = x
            // >0 : y > x

            if (scoreComparison != 0)
            {
                return scoreComparison;
            }
            else
            {
                // 相同则比较customerId，从小到大排序
                return x.CustomerId.CompareTo(y.CustomerId);
            }
        }
    }
}
