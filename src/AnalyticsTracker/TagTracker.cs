using System.Collections.Generic;
using System.Text;

namespace Vertica.AnalyticsTracker
{
    public class TagTracker
    {
        private string _account;
        private string _dataLayerName;
        private readonly List<MessageBase> _messages;
        private string _environmentAuth;
        private string _environmentPreview;

        public TagTracker()
        {
            _messages = new List<MessageBase>();
        }

        public void SetAccount(string account)
        {
            _account = account;
        }

        public void SetDataLayerName(string dataLayerName)
        {
            _dataLayerName = dataLayerName;
        }

        public void AddMessage(MessageBase message)
        {
            _messages.Add(message);
        }

        public string Render()
        {
            var sb = new StringBuilder();
            RenderDataLayer(sb);
	        RenderNoScript(sb);
	        RenderScript(sb);
            return sb.ToString();
        }

	    private string EnvironmentQueryParameter
	    {
		    get
		    {
			    var environmentQueryParameter = _environmentAuth != null && _environmentPreview != null
				    ? string.Format("&gtm_auth={0}&gtm_preview={1}", _environmentAuth, _environmentPreview)
				    : string.Empty;
			    return string.Format("{0}&gtm_cookies_win=x", environmentQueryParameter);
		    }
	    }

	    public string RenderScript()
	    {
			var sb = new StringBuilder();
			RenderScript(sb);
			return sb.ToString();
		}

	    public string RenderNoScript()
		{
			var sb = new StringBuilder();
			RenderNoScript(sb);
			return sb.ToString();
		}

	    public string RenderDataLayer()
		{
			var sb = new StringBuilder();
			RenderDataLayer(sb);
			return sb.ToString();
		}

	    public string RenderHeader()
        {
            var sb = new StringBuilder();
            RenderMessages(sb);

            return sb.ToString();
        }

	    private void RenderScript(StringBuilder sb)
	    {
		    sb.AppendFormat(@"<!-- Google Tag Manager -->
<script>(function(w,d,s,l,i){{w[l]=w[l]||[];w[l].push({{'gtm.start':
new Date().getTime(),event:'gtm.js'}});var f=d.getElementsByTagName(s)[0],
j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
'https://www.googletagmanager.com/gtm.js?id='+i+dl+'{2}';f.parentNode.insertBefore(j,f);
}})(window,document,'script','{1}','{0}');</script>
<!-- End Google Tag Manager -->", _account, _dataLayerName, EnvironmentQueryParameter);
	    }

	    private void RenderNoScript(StringBuilder sb)
	    {
		    sb.AppendFormat(@"
<!-- Google Tag Manager (noscript) -->
<noscript><iframe src='https://www.googletagmanager.com/ns.html?id={0}{1}'
height='0' width='0' style='display:none;visibility:hidden'></iframe></noscript>
<!-- End Google Tag Manager (noscript) -->", _account, EnvironmentQueryParameter);
	    }

	    private void RenderDataLayer(StringBuilder sb)
        {
            sb.AppendLine("<script>");
            sb.Append($"var {_dataLayerName} = []; function tagManagerPush(obj){{{_dataLayerName}.push(obj);}}");
            sb.AppendLine();
            RenderMessages(sb);
            sb.AppendLine("</script>");
        }

        private void RenderMessages(StringBuilder sb)
        {
            foreach (var message in _messages)
            {
                sb.AppendLine(message.RenderMessage());
            }
        }

        public void SetEnvironmentAuth(string environmentAuth)
        {
            _environmentAuth = environmentAuth;
        }

        public void SetEnvironmentPreview(string environmentPreview)
        {
            _environmentPreview = environmentPreview;
        }
    }
}