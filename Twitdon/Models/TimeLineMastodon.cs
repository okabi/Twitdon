﻿using log4net;
using Mastonet;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Twitdon.Interfaces;

namespace Twitdon.Models
{
    /// <summary>
    /// Mastodon タイムラインを表すクラスです。
    /// </summary>
    class TimeLineMastodon : ITimeLine
    {
        #region フィールド

        /// <summary>
        /// ロガーオブジェクト。
        /// </summary>
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// クライアントの本体。
        /// </summary>
        private TwitdonMastodonClient client;

        /// <summary>
        /// ステータスのコントロールリストの本体。
        /// </summary>
        private List<TimeLineStatus> statuses;

        /// <summary>
        /// タイムラインストリーミング。
        /// </summary>
        private readonly TimelineStreaming streaming;

        #endregion

        #region プロパティ

        /// <summary>
        /// タイムラインを表示するパネルです。
        /// </summary>
        public Panel Panel { get; set; }

        /// <summary>
        /// タイムラインに保存されているステータスです。
        /// </summary>
        /// <param name="i">古いものから順になっているインデックス。</param>
        /// <returns>ステータスのコントロール。</returns>
        public TimeLineStatus this[int i]
        {
            get
            {
                return statuses[i];
            }
        }

        /// <summary>
        /// タイムラインに保存されているステータスコントロールの数です。
        /// </summary>
        public int Count
        {
            get
            {
                return statuses.Count;
            }
        }

        /// <summary>
        /// タイムライン枠に表示するタイムライン名です。
        /// </summary>
        public string TimeLineName { get; private set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="client">Mastodon クライアント。</param>
        /// <param name="streaming">イベントの設定されたストリーミング。</param>
        /// <param name="name">タイムライン名。</param>
        public TimeLineMastodon(TwitdonMastodonClient client, TimelineStreaming streaming, string name)
        {
            this.client = client;
            this.streaming = streaming;
            TimeLineName = $"{name}{client.AccountName}";
            statuses = new List<TimeLineStatus>(Define.StatusesCapacity);
        }

        #endregion

        #region public メソッド

        /// <summary>
        /// タイムラインにステータスコントロールを追加します。キャパシティオーバーして古いステータスを削除した場合 true を返します。
        /// </summary>
        /// <param name="status">追加するステータス。</param>
        public void AddStatus(IStatus status)
        {
            if (Panel == null)
            {
                logger.Error("タイムライン枠が未割り当てです。");
                Utilities.ShowError("タイムライン枠が未割り当てです。");
                return;
            }
            if (statuses.Count >= Define.StatusesCapacity)
            {
                Panel.Controls.RemoveAt(0);
                statuses.RemoveAt(0);
            }
            var st = new TimeLineStatus(status);
            st.Size = new Size(Panel.ClientSize.Width, 130);
            st.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            statuses.Add(st);
            Panel.Controls.Add(st);
        }

        /// <summary>
        /// ストリーミングを開始します。
        /// このメソッドを実行する前にイベントを登録してください。
        /// </summary>
        public void Start()
        {
            try
            {
                streaming.Start();
            }
            catch (Exception e)
            {
                logger.ErrorFormat($"ストリーミングの取得に失敗しました。インターネット接続を確認してください。{e.Message}");
                Utilities.ShowError($"ストリーミングの取得に失敗しました。\nインターネット接続を確認してください。");
            }
        }

        /// <summary>
        /// ストリーミングを終了します。
        /// </summary>
        public void Stop()
        {
            streaming.Stop();
        }

        /// <summary>
        /// ストリーミングで新しいステータスを得た時の動作を追加します。
        /// </summary>
        /// <param name="function">追加する動作。</param>
        public void AddOnUpdate(EventHandler<StreamUpdateEventArgs> function)
        {
            streaming.OnUpdate += function;
        }

        /// <summary>
        /// ストリーミングで新しい通知を得た時の動作を追加します。
        /// </summary>
        /// <param name="function">追加する動作。</param>
        public void AddOnNotification(EventHandler<StreamNotificationEventArgs> function)
        {
            streaming.OnNotification += function;
        }

        /// <summary>
        /// ストリーミングで新しい削除通知を得た時の動作を追加します。
        /// </summary>
        /// <param name="function">追加する動作。</param>
        public void AddOnDelete(EventHandler<StreamDeleteEventArgs> function)
        {
            streaming.OnDelete += function;
        }

        #endregion
    }
}