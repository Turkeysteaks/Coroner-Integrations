using System;
using System.Collections;

namespace CoronerBiodiversity.Patch.Aloe;

public class SimpleEnumerator : IEnumerable
{
    public IEnumerator enumerator;
    public Action prefixAction, postfixAction;
    public Action<object> preItemAction, postItemAction;
    public Func<object, object> itemAction;
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    public IEnumerator GetEnumerator()
    {
        prefixAction();
        while (enumerator.MoveNext())
        {
            var item = enumerator.Current;
            preItemAction(item);
            yield return itemAction(item);
            postItemAction(item);
        }
        postfixAction();
    }
}