﻿using System.Threading.Tasks;

namespace Twitdon.Interfaces
{
    /// <summary>
    /// Twitter / Mastodon API を利用するためのクライアントを定義するインターフェースです。
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// ツイート/トゥートします。
        /// </summary>
        /// <param name="status">ツイート/トゥートする内容。</param>
        /// <returns>ツイート/トゥート内容。</returns>
        Task<IStatus> PostStatus(string status);

        /// <summary>
        /// ツイート/トゥートをお気に入り登録します。
        /// </summary>
        /// <param name="statusId">お気に入り登録するツイート/トゥートのID。</param>
        /// <returns>ツイート/トゥート内容。</returns>
        Task<IStatus> Favourite(int statusId);

        /// <summary>
        /// ツイート/トゥートをお気に入り解除します。
        /// </summary>
        /// <param name="statusId">お気に入り解除するツイート/トゥートのID。</param>
        /// <returns>ツイート/トゥート内容。</returns>
        Task<IStatus> Unfavourite(int statusId);
    }
}