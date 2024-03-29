/*
 * AbstractPayload.cs
 * SnowplowTracker.Payloads
 * 
 * Copyright (c) 2015 Snowplow Analytics Ltd. All rights reserved.
 *
 * This program is licensed to you under the Apache License Version 2.0,
 * and you may not use this file except in compliance with the Apache License Version 2.0.
 * You may obtain a copy of the Apache License Version 2.0 at http://www.apache.org/licenses/LICENSE-2.0.
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the Apache License Version 2.0 is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the Apache License Version 2.0 for the specific language governing permissions and limitations there under.
 * 
 * Authors: Joshua Beemster, Paul Boocock
 * Copyright: Copyright (c) 2015-2019 Snowplow Analytics Ltd
 * License: Apache License Version 2.0
 */

/*
 * Modified by AlgebraAI on 2024-01-31
 * - Added priority and eventIndex fields
 */

using System.Collections.Generic;

namespace GametunerTracker.Payloads {
	internal abstract class AbstractPayload : IPayload {

		protected Dictionary<string, object> payload = new Dictionary<string, object>();
        protected int priority = 0;
		protected int eventIndex = 0;

        /// <summary>
        /// Gets the dictionary within the Payload
        /// </summary>
        /// <returns>The payload</returns>
        public Dictionary<string, object> GetDictionary() {
			return payload;
		}

		/// <summary>
		/// Gets the byte size of the key-value pairs in the payload
		/// </summary>
		/// <returns>The total byte size</returns>
		public long GetByteSize() {
			return Utils.GetUTF8Length(ToString());
		}

		/// <summary>
		/// Returns a JSON string representing the payload.
		/// </summary>
		/// <returns>A JSON string representing the payload.</returns>
		public override string ToString() {
			return Utils.DictToJSONString(payload);
		}
	}
}
